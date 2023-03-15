﻿namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly IPostService _postService;
        private readonly ILikeService _likeService;

        public PostController(IPostService postService, ILikeService likeService)
        {
            _postService = postService;
            _likeService = likeService;
        }

        [Authorize(Role.User, Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            try
            {
                List<PostResponse> posts = await _postService.GetAllPostsAsync();

                if (posts.Count == 0)
                {
                    return NoContent();

                }
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.User, Role.Admin)]
        [HttpGet]
        [Route("{postId}")]
        public async Task<IActionResult> GetPostByPostId([FromRoute] int postId)
        {
            try
            {
                var postResponse = await _postService.GetPostByPostIdAsync(postId);

                if (postResponse == null)
                {
                    return NotFound();
                }
                return Ok(postResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.User, Role.Admin)]
        [HttpGet]
        [Route("user/{userId}")]
        public async Task<IActionResult> GetAllPostsByUserId([FromRoute] int userId)
        {
            try
            {
                var postResponse = await _postService.GetAllPostsByUserIdAsync(userId);

                if (postResponse == null)
                {
                    return NotFound();
                }
                return Ok(postResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }




        [Authorize(Role.User, Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostRequest newPost)
        {
            try
            {
                var postResponse = await _postService.CreatePostAsync(newPost);

                return Ok(postResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [Authorize(Role.User, Role.Admin)]
        [HttpPut]
        [Route("{postId}")]
        public async Task<IActionResult> UpdatePost([FromRoute] int postId, [FromBody] PostUpdateRequest updatedPost)
        {
            try
            {
                var postResponse = await _postService.UpdatePostAsync(postId, updatedPost);

                if (postResponse == null)
                {
                    return NotFound();
                }

                return Ok(postResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.User, Role.Admin)]
        [HttpDelete]
        [Route("{postId}")]
        public async Task<IActionResult> DeletePost([FromRoute] int postId)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["User"];

                if (currentUser != null && !currentUser.User.Posts.Exists(x => x.PostId == postId) && currentUser.Role != Role.Admin)
                {
                    return Unauthorized(new { message = "Unauthorized" });
                }

                var postResponse = await _postService.DeletePostAsync(postId);

                if (postResponse == null)
                {
                    return NotFound();
                }

                return Ok(postResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }

}
