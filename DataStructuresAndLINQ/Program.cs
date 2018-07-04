using DataStructuresAndLINQ.DataStructures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace DataStructuresAndLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            bool DataFetched = false;
            IEnumerable<User> usersList = null;

            while (!DataFetched)
            {
                Console.WriteLine("Hello, welcome to my DataStructureAndLinq Program that works with" +
               " userList. Structure of data is User->Address,ToDos,Posts->Comments" +
               "First we need to fetch data from given endpoints. If you redy press Enter");
                Console.ReadLine();

                usersList = GetUserListStructure();
                if (usersList != null)
                {
                    DataFetched = true;
                    Console.WriteLine("Data is successfully fetched from endpoints");
                }
            }
            bool IsWork = true;

            Console.WriteLine("Now we can process this data.");

            while (IsWork)
            {
                Console.WriteLine(
                    "Please chose the option from the list above!" + "\n" +
                    "1) Get the number of comments under the posts of a specific" +
                    " user (by iDi) (list from post-number)" + "\n" +
                    "2) Get a list of comments under the posts of a specific user (via iDi)," +
                    " where the body comment is <50 characters (list from the comments)" + "\n" +
                    "3) Get the list (id, name) from the todos list that is executed for a particular user (by iDi)" + "\n" +
                    "4) Get list of users alphabetically (ascending) with sorted todo items by length name (descending)" + "\n" +
                    "5) Get the following Post structure (pass the post's Id to the parameters)" + "\n" +
                    "6) Get the following  User structure (pass the user's Id to the parameters)" + "\n" +
                    "7) Finish to work with program." + "\n" +
                    "Please enter ONLY number of choosen option!!!");

                int choose;
                try
                {
                      choose = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("------------------------"+"\n"+
                        "You have char in input instead of int. Please enter valid input"+
                        "\n"+"---------------------------");
                    continue;
                }

                switch (choose)
                {
                    case 1:
                        Console.WriteLine("You choose option " + "\n" +
                            "1) Get the number of comments under the posts of a specific" +
                            " user (by iDi) (list from post-number)" + "\n" +
                            "Please enter userId");

                        int userId1;
                        try
                        {
                            userId1 = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("------------------------" + "\n" +
                                "You have char in input instead of int. Please enter valid input" +
                                "\n" + "---------------------------");
                            break;
                        }


                        int count = GetPostsCount(usersList, userId1);
                        if (count != -1)
                        {
                            Console.WriteLine("User has =" + count + "= posts" + "\n" +
                                "---------------------------" + "\n");
                        }
                        break;
                    case 2:
                        Console.WriteLine("You choose option " + "\n" +
                            "2) Get a list of comments under the posts of a specific user (via iDi)," +
                    " where the body comment is <50 characters (list from the comments)" + "\n" +
                            "Please enter userId");

                        int userId2;
                        try
                        {
                            userId2 = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("------------------------" + "\n" +
                                "You have char in input instead of int. Please enter valid input" +
                                "\n" + "---------------------------");
                            break;
                        }


                        var comments = GetPostComentsLessThenFiFtyLength(usersList, userId2);
                        if (comments != null)
                        {
                            foreach (Comment c in comments)
                            {
                                Console.WriteLine("Id:" + c.Id + "\n" + c.CreatedAt + "\n"
                                    + "Comment body:" + c.Body + "\n" + "Likes:" + c.Likes + "\n");
                            }
                            Console.WriteLine("---------------------------" + "\n");
                        }
                        break;
                    case 3:
                        Console.WriteLine("You choose option " + "\n" +
                            "3) Get the list (id, name) from the todos list that is executed for a particular user (by iDi)" + "\n" +
                            "Please enter userId");
                        int userId3;
                        try
                        {
                            userId3 = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("------------------------" + "\n" +
                                "You have char in input instead of int. Please enter valid input" +
                                "\n" + "---------------------------");
                            break;
                        }

                        var doneTodos = GetDoneToDos(usersList, userId3);
                        if (doneTodos!=null)
                        {
                            Console.WriteLine("Done todos: " + "\n");

                            foreach (KeyValuePair<int, string> todo in doneTodos)
                            {
                                Console.WriteLine( "Id:  " + todo.Key + "    name: " + todo.Value + "\n");
                            }
                            Console.WriteLine("---------------------------" + "\n");
                        }
                        break;
                    case 4:
                        Console.WriteLine("You choose option " + "\n" +
                            "4) Get list of users alphabetically (ascending) with sorted todo items by length name (descending)" + "\n");
                        var userList = GetSortedUserAndToDos(usersList);
                        if (userList != null)
                        {
                            Console.WriteLine("Sorted users list with sorted todos: " + "\n");

                            foreach (User u in userList)
                            {
                                Console.WriteLine("UserName:  " + u.Name + "\n");
                                if (u.Todos.Count()!=0)
                                {
                                    Console.WriteLine("Done todos: " + "\n");
                                    foreach (Todo todo in u.Todos)
                                    {
                                        Console.WriteLine("* " + todo.Name + "\n");
                                    }
                                }
                                Console.WriteLine("--------------" + "\n");
                            }
                            
                        }
                            break;
                    case 5:
                        Console.WriteLine("You choose option " + "\n" +
                           "5) Get the following Post structure (pass the post's Id to the parameters)" + "\n" +
                           "Please enter postId");

                        int postId;
                        try
                        {
                            postId = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("------------------------" + "\n" +
                                "You have char in input instead of int. Please enter valid input" +
                                "\n" + "---------------------------");
                            break;
                        }


                        var post = GetPostResponse(usersList, postId);
                        if (post != null)
                        {
                            Console.WriteLine("Post Response: " + "\n"+
                                "MostLikedComment :  "+"\n"+post.TheMostLikedComment+"\n"+
                                "The biggest comment :   "+"\n"+post.TheBiggestComment+"\n"+
                                "The number of comments under the post where or 0 words or" +
                                " the length of the text is <80 :   " + post.QuantityOfNeededComment);
                            Console.WriteLine("---------------------------" + "\n");
                        }
                        break;
                    case 6:
                        Console.WriteLine("You choose option " + "\n" +
                           "6) Get the following  User structure (pass the user's Id to the parameters)" + "\n" +
                           "Please enter userId");
                        int userId;
                        try
                        {
                            userId = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("------------------------" + "\n" +
                                "You have char in input instead of int. Please enter valid input" +
                                "\n" + "---------------------------");
                            break;
                        }

                        var user = GetUserResponse(usersList, userId);
                        if (user != null)
                        {
                            Console.WriteLine("User Response: " + "\n" +
                                "Quantity of comments above last post :  " + "\n" + user.QuantityComentsAtLastPost + "\n" +
                                "Quantity of not done todos :   " + "\n" + user.QuantityOfNotDoneTodos);
                            if (user.MostPopularPostWithLikes != null)
                            {
                                Console.WriteLine("Most popular Post (likes) :   " + "\n" + "-" + user.MostPopularPostWithLikes.Body);
                            }
                            if (user.MostPopularPostWithComments != null)
                            {
                             
                                Console.WriteLine("Most popular Post (comments) :   " + "\n" + "-" + user.MostPopularPostWithComments.Body);
                            }
                            if (user.TheLatestPost != null)
                            {
                                Console.WriteLine("The latest Post :   " +"\n"+"-"+ user.TheLatestPost.Body);
                            }
                                
                            Console.WriteLine("---------------------------" + "\n");
                        }
                        break;
                    case 7:
                        IsWork = false;
                        break;
                    default:
                        Console.WriteLine("Sorry, there is no such option. Please choose another!");
                        break;
                }

            }
            Console.WriteLine("Thanks for using my program. Hope you enjoy it ;)");
            Console.ReadKey();
        }
        public static int GetPostsCount(IEnumerable<User> users, int userId)
        {
            var user = users.Where(u => u.Id == userId).FirstOrDefault();
            if (user == null)
            {
                Console.WriteLine("There is no such user" + "\n" +
                    "---------------------------" + "\n");
                return -1;//it is possible to throw exception here 
            }
            var count = user.Posts.Count();
            return count;
        }

        public static IEnumerable<Comment> GetPostComentsLessThenFiFtyLength(IEnumerable<User> users, int userId)
        {
            var user = users.Where(u => u.Id == userId).FirstOrDefault();
            if (user != null)
            {
                var commments = users.SelectMany(u => u.Posts
                    .SelectMany(p => p.Comments).Where(c => c.Body.Length < 100 && c.UserId == 81)).ToList();
                if (commments == null)
                {
                    Console.WriteLine("There is no comments with this" +
                        " requirements for selected user " + "\n" +
                        "---------------------------" + "\n");
                    return null;
                }
                return commments;
            }
            Console.WriteLine("There is no such user" + "\n" +
                    "---------------------------" + "\n");
            return null;
        }

        public static Dictionary<int, string> GetDoneToDos(IEnumerable<User> users, int userId)
        {
            var user = users.Where(u => u.Id == userId).FirstOrDefault();
            if (user != null)
            {
                var doneTodos = user.Todos
                        .Where(td => td.IsComlete == true)
                        .Select(res => new { res.Id, res.Name })
                        .ToDictionary(t => t.Id, t => t.Name);
                if (doneTodos.Count() == 0)
                {
                    Console.WriteLine("There is no done todos for selected user" + "\n" +
                    "---------------------------" + "\n");
                    return null;
                }
                return doneTodos;
            }
            Console.WriteLine("There is no such user" + "\n" +
                    "---------------------------" + "\n");
            return null;
        }

        public static IEnumerable<User> GetSortedUserAndToDos(IEnumerable<User> users)
        {
            if (users == null)
            {
                Console.WriteLine("There is no users in list" + "\n" +
                    "---------------------------" + "\n");
                return null;
            }

            var res = users
            .OrderBy(user => user.Name)
            .Select(user => new User
            {
                Id = user.Id,
                Todos = user.Todos.Select(x => x).OrderByDescending(todo => todo.Name).ToList(),
                Name = user.Name,
                Avatar = user.Avatar,
                CreatedAt = user.CreatedAt,
                Email = user.Email
            }).ToList();
            return res;
        }

        public static PostResponse GetPostResponse(IEnumerable<User> users, int postId)
        {
            var post = users.SelectMany(u => u.Posts)
                .Where(p => p.Id == postId).FirstOrDefault();
            if (post == null)
            {
                Console.WriteLine("There is no such post" + "\n" +
                    "---------------------------" + "\n");
                return null;
            }
            var resp = new PostResponse
            {
                TheBiggestComment = post.Comments.OrderByDescending(c => c.Body.Length)
                   .Select(x => x.Body).FirstOrDefault(),
                TheMostLikedComment = post.Comments
                   .OrderByDescending(c => c.Likes)
                   .Select(x => x.Body).FirstOrDefault(),
                QuantityOfNeededComment = post.Comments.Count(c => c.Likes == 0 || c.Body.Length <= 80)

            };
            return resp;

        }

        public static UserResponse GetUserResponse(IEnumerable<User> users, int userId)
        {
            var user = users.Where(u => u.Id == userId).FirstOrDefault();
            if (user == null)
            {
                Console.WriteLine("There is no such user" + "\n" +
                    "---------------------------" + "\n");
                return null;
            }

            var resp = new UserResponse
            {
                QuantityOfNotDoneTodos = user.Todos.Where(todo => todo.IsComlete == false).Count(),
                TheLatestPost = user.Posts.OrderByDescending(x => x.createdAt).FirstOrDefault(),
                QuantityComentsAtLastPost = user.Posts.OrderByDescending(x => x.createdAt)
                    .FirstOrDefault()?.Comments?.Count() ??0,
                MostPopularPostWithComments = user.Posts
                    .OrderByDescending(x => x.Comments).FirstOrDefault(x => x.Body.Length > 80),
                MostPopularPostWithLikes = user.Posts
                    .OrderByDescending(x => x.Likes).FirstOrDefault()
            };

            return resp;
        }

        public class PostResponse
        {
            public string TheBiggestComment { get; set; }
            public string TheMostLikedComment { get; set; }
            public int QuantityOfNeededComment { get; set; }
        }
        public class UserResponse
        {
            public Post TheLatestPost { get; set; }
            public Post MostPopularPostWithLikes { get; set; }
            public Post MostPopularPostWithComments { get; set; }
            public int QuantityOfNotDoneTodos { get; set; }
            public int QuantityComentsAtLastPost { get; set; }
        }

        public static IEnumerable<User> GetUserListStructure()
        {

            HttpClient client = new HttpClient();

            List<UserModel> users = null;
            List<PostModel> posts = null;
            List<AddressModel> addresses = null;
            List<CommentModel> comments = null;
            List<TodoModel> todos = null;

            #region UsersResponse
            var usersResponse = client.GetAsync("https://5b128555d50a5c0014ef1204.mockapi.io/users")
                .ContinueWith((taskwithresponse) =>
                {
                    var resp = taskwithresponse.Result;
                    var jsonString = resp.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    users = JsonConvert.DeserializeObject<List<UserModel>>(jsonString.Result);

                });
            usersResponse.Wait();
            #endregion
            #region PostsResponse
            var postsResponse = client.GetAsync("https://5b128555d50a5c0014ef1204.mockapi.io/posts")
                .ContinueWith((taskwithresponse) =>
                {
                    var resp = taskwithresponse.Result;
                    var jsonString = resp.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    posts = JsonConvert.DeserializeObject<List<PostModel>>(jsonString.Result);

                });
            postsResponse.Wait();
            #endregion
            #region CommentsResponse
            var commentsResponse = client.GetAsync("https://5b128555d50a5c0014ef1204.mockapi.io/comments")
                .ContinueWith((taskwithresponse) =>
                {
                    var resp = taskwithresponse.Result;
                    var jsonString = resp.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    comments = JsonConvert.DeserializeObject<List<CommentModel>>(jsonString.Result);

                });
            commentsResponse.Wait();
            #endregion
            #region TodosResponse
            var todosResponse = client.GetAsync("https://5b128555d50a5c0014ef1204.mockapi.io/todos")
                .ContinueWith((taskwithresponse) =>
                {
                    var resp = taskwithresponse.Result;
                    var jsonString = resp.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    todos = JsonConvert.DeserializeObject<List<TodoModel>>(jsonString.Result);

                });
            todosResponse.Wait();
            #endregion
            #region AddressesResponse
            var addressesResponse = client.GetAsync("https://5b128555d50a5c0014ef1204.mockapi.io/address")
                .ContinueWith((taskwithresponse) =>
                {
                    var resp = taskwithresponse.Result;
                    var jsonString = resp.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    addresses = JsonConvert.DeserializeObject<List<AddressModel>>(jsonString.Result);

                });
            addressesResponse.Wait();
            #endregion
            if (users == null || posts == null || addresses == null || comments == null || todos == null)
            {
                Console.WriteLine("There were some errors while fetching data");
                return null;
            }
            var MyStructure = users.Select(user => new User
            {
                Avatar = user.Avatar,
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                Address = addresses.Where(addr => addr.UserId == user.Id)
                                  .Select(address => new Address
                                  {
                                      Id = address.Id,
                                      City = address.City,
                                      Country = address.Country,
                                      Zip = address.Zip,
                                      Street = address.Street
                                  }).FirstOrDefault(),
                Posts = posts.Where(post => post.UserId == user.Id)
                                  .Select(x => new Post
                                  {
                                      Id = x.Id,
                                      Body = x.Body,
                                      createdAt = x.createdAt,
                                      Likes = x.Likes,
                                      Title = x.Title,
                                      UserId = x.UserId,
                                      Comments = comments.Where(comment => comment.PostId == x.Id)
                                      .Select(y => new Comment
                                      {
                                          Id = y.Id,
                                          Body = y.Body,
                                          UserId = y.UserId,
                                          PostId = y.PostId,
                                          CreatedAt = y.CreatedAt,
                                          Likes = y.Likes
                                      }).ToList()
                                  }).ToList(),
                Todos = todos.Where(todo => todo.UserId == user.Id)
                                  .Select(td => new Todo
                                  {
                                      Id = td.Id,
                                      CreatedAt = td.CreatedAt,
                                      IsComlete = td.IsComlete,
                                      UserId = td.UserId,
                                      Name = td.Name
                                  }).ToList()
            }).ToList();

            return MyStructure;
        }
    }
}
