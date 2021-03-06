﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDS.Model;
using MDS.Model.Attributes;
using System.ComponentModel.DataAnnotations;
using MDS.Web.HtmlHelper.Attributes;

namespace MDS.Blog
{

    public class Article : MDS.Model.Model
    {
        [PrimaryKey]
        [IdentifyKey]
        [Remove]
        public int ID { get; set; }

        [Display(Name="标题")]
        public string Title { get; set; }

        [Display(Name = "内容")]
        public string Content { get; set; }

        [Display(Name = "发布时间")]
        [HiddenEdit]
        public DateTime PostTime { get; set;}

        [Display(Name = "阅读次数")]
        [HiddenEdit]
        public int Reads { get; set; }
    }
}
