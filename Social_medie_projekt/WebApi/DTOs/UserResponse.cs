﻿namespace WebApi.DTOs
{
    public class UserResponse
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public UserLoginResponse Login { get; set; }

        public UserImageResponse? Image { get; set; }

        public List<UserPostResponse> Posts { get; set; } = new();

        public List<UserFollowResponse> Follow { get; set; } = new();
    }

    public class UserLoginResponse
    {
        public int LoginId { get; set; }

        public string Email { get; set; } = string.Empty;

        public Role Role { get; set; }
    }

    public class UserImageResponse
    {
        public byte[]? Image { get; set; }
    }

    public class UserPostResponse
    {
        public int PostId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Desc { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public UserPostPostLikesResponse PostLikes { get; set; }
    }

    public class UserPostPostLikesResponse
    {
        public int Likes { get; set; }
    }

    public class UserFollowResponse
    {
        public int UserId { get; set; }

        public int FollowingId { get; set; }
    }
}
