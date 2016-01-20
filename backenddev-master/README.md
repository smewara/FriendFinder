<b>Features</b> : 

* Ability to friend and unfriend people
* Get a list of a user's friends
* Get a list of a user's potential friends
* Find connection between two people. Susan->Sarah->Joe->Hardy

The solution consists of UserController, FriendsController & FriendNetworkController. Most of the methods accept a "Request" parameter and return a "Response" parameter with status "Sucess" or "Error" 
with additional message in case of an exception. Test cases written to test each of the major functions.

You can get a visual of your API by visiting http://localhost/swagger/ui/index whenever you build.

UsersController methods :

	Add new user : provide username and password in request parameter

	GetAllUsers : Get all users in the network

	Get a specific user : provide userid.


FriendsController methods :

	Add a friend : provide userfriendrequest parameter. specify two user ids to want to be friends of each other.

	Remove a friend : takes in a user id and another id that is a friend's id.

	Get All friends of a user : specify user id

	Find connection between two users : provide userA id and userB id. Get all connecting paths between userA and userB except those that have a shorter path subset. Susan->Sarah->Joe->Hardy.

<b>PotentialFriendsController methods :</b>

	Get all potential fiends of a user : specify user id you want to find potential friends of. Get all friends of friends of friends till all possible connected nodes from a user can be reached.


