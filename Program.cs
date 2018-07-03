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
            var userList = GetUserListStructure();

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
        public static int GetPostsCount(IEnumerable<User> users, int userId)
        {
            return users.Where(u => u.Id == userId).Select(user => user.Posts).Count();
        }

        public static IEnumerable<Comment> GetPostComentsLessThenFiFtyLength(IEnumerable<User> users, int userId)
        {
            var commments = users.SelectMany(u => u.Posts
            .SelectMany(p => p.Comments).Where(c => c.Body.Length < 100 && c.UserId == 81)).ToList();
            return commments;
        }

        public static Dictionary<int, string> GetDoneToDos(IEnumerable<User> users, int userId)
        {
            var result = users.Where(u => u.Id == userId).SelectMany(user => user.Todos)
               .Where(td => td.IsComlete == true)
           .Select(res => new { res.Id, res.Name })
                  .ToDictionary(t => t.Id, t => t.Name);

            return result;
        }

        public static IEnumerable<User> GetSortedUserAndToDos(IEnumerable<User> users)
        {
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
            var resp = users.SelectMany(u => u.Posts)
                .Where(p => p.Id == postId)
                .Select(post => new PostResponse
                {
                    TheBiggestComment = post.Comments.OrderByDescending(c => c.Body.Length)
                    .Select(x => x.Body).FirstOrDefault(),
                    TheMostLikedComment = post.Comments
                    .OrderByDescending(c => c.Likes)
                    .Select(x => x.Body).FirstOrDefault(),
                    QuantityOfNeededComment=post.Comments.Count(c=>c.Likes==0 || c.Body.Length<=80)
                    
                }).FirstOrDefault();

            return resp;
        }

        public static UserResponse GetUserREsponse(IEnumerable<User> users, int userId)
        {
            var resp = users
                .Where(user => user.Id == userId)
                .Select(u => new UserResponse
                {
                    QuantityOfNotDoneTodos = u.Todos.Where(todo => todo.IsComlete == false).Count(),
                    TheLatestPost = u.Posts.OrderByDescending(x => x.createdAt).FirstOrDefault(),
                    QuantityComentsAtLastPost = u.Posts.OrderByDescending(x => x.createdAt)
                    .FirstOrDefault().Comments.Count(),
                    MostPopularPostWithComments = u.Posts
                    .OrderByDescending(x => x.Comments).FirstOrDefault(x => x.Body.Length > 80),
                    MostPopularPostWithLikes = u.Posts
                    .OrderByDescending(x => x.Likes).FirstOrDefault()
                }).FirstOrDefault();

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
