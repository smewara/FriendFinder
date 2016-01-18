using System.Collections.Generic;
using System.Linq;
using FriendFinder.Data.Data;
using FriendFinder.Data.Responses;

namespace FriendFinder.Database.Repositories
{
  
    public sealed class FriendNetworkRepo : IFriendNetworkRepo
    {
        private sealed class GraphUser
        {
            private readonly UserResponse _user;
            readonly int _level;
            public GraphUser(UserResponse user, int level)
            {
                _user = user;
                _level = level;
            }

            public UserResponse User { get { return _user; } }
            public int Level { get { return _level; } }

            public override string ToString()
            {
                return string.Format("UserId : {0} Level : {1}", _user.UserId, _level);
            }
        }
        private sealed class GraphResponse
        {
            private readonly IEnumerable<IEnumerable<UserResponse>> _usersList;
            private readonly IDictionary<int, GraphUser> _visitedNodes;
            public GraphResponse(IEnumerable<IEnumerable<UserResponse>> users, IDictionary<int, GraphUser> visitedNodes)
            {
                _usersList = users;
                _visitedNodes = visitedNodes;
            }
            public IEnumerable<IEnumerable<UserResponse>> UsersList { get { return _usersList; } }
            public IEnumerable<KeyValuePair<int, GraphUser>> VisitedNodes { get { return _visitedNodes; } }
        }

        private readonly IUserRepo _userRepo;
      

        public FriendNetworkRepo(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        //Using BFS
        private GraphResponse BreadthFirstSearchGraph(int userId, int? potentialFriendId = null)
        {
            var queue = new Queue<User>();
            var parents = new Dictionary<int, UserResponse>();
            var allPossiblePaths = new List<List<UserResponse>>();
            var visitedNodes = new Dictionary<int, GraphUser>();
            var level = 0;
           
            var user = _userRepo.FindById(userId);

            if (user == null) return null;

            if (!visitedNodes.ContainsKey(user.Id))
                visitedNodes.Add(user.Id, new GraphUser(new UserResponse(user), level));

            parents.Add(user.Id, null);

            queue.Enqueue(user);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (visitedNodes.ContainsKey(node.Id))
                {
                    level = visitedNodes[node.Id].Level;
                    level++;
                }

                foreach (var frd in node.Friends)
                {
                    if (visitedNodes.ContainsKey(frd.Id)) continue;

                    if (!parents.ContainsKey(frd.Id))
                        parents.Add(frd.Id, null);

                    parents[frd.Id] = new UserResponse(node);

                    if (frd.Id == potentialFriendId)
                    {
                        //Found;

                        var li = new List<UserResponse>();
                        
                        TraverseUpwards(new UserResponse(frd), ref li, ref parents);

                        li.Reverse();

                        allPossiblePaths.Add(li);
                    }
                    else
                    {
                        if (!visitedNodes.ContainsKey(frd.Id))
                            visitedNodes.Add(frd.Id, new GraphUser(new UserResponse(frd), level));

                        queue.Enqueue(frd);
                    }

                    
                }

            }


            return new GraphResponse(allPossiblePaths, visitedNodes);   

        }

        private static void TraverseUpwards(UserResponse node, ref List<UserResponse> nodeList, ref Dictionary<int, UserResponse> parents)
        {
            while (node != null)
            {
                nodeList.Add(node);

                UserResponse parentNode;

                parents.TryGetValue(node.UserId, out parentNode);

                node = parentNode;

            }
        }

        public IEnumerable<IEnumerable<UserResponse>> GetAllPaths(int userIdA, int userIdB)
        {
           
                var graphResponse = BreadthFirstSearchGraph(userIdA, userIdB);

                return graphResponse.UsersList;
            
        }

        public IList<UserResponse> GetPotentialFriends(int userId)
        {
            var nodes = BreadthFirstSearchGraph(userId);

            return (from node in nodes.VisitedNodes 
                    where node.Value.Level > 1 
                    select node.Value.User).ToList();
          
        }
    }
}
