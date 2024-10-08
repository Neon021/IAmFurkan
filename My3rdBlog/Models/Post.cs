﻿using Microsoft.AspNetCore.Identity;
using MyBlog.Data.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Blog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        [AllowHtml]
        public string Body { get; set; } = "";
        public string Image { get; set; } = "";
        public string Description { get; set; } = "";
        public string Tags { get; set; } = "";
        public string Category { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public List<MainComment> MainComments { get; set; }
    }
}
