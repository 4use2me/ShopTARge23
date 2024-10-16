﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge23.Core.Dto
{
    public class SpaceshipDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Typename { get; set; }
        public string? SpaceshipModel { get; set; }
        public DateTime? BuiltDate { get; set; }
        public int? Crew { get; set; }
        public int? EnginePower { get; set; }
        public List<IFormFile> Files { get; set; }
        public IEnumerable<FileToApiDto> FilesToApiDtos { get; set; }
            = new List<FileToApiDto>();


        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
