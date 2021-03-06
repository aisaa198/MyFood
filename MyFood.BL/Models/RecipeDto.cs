﻿using MyFood.Common.Enums;
using System;
using System.Collections.Generic;

namespace MyFood.BL.Models
{
    public class RecipeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public string[] ListOfIngredients { get; set; }
        public string Directions { get; set; }
        public List<UserDto> Users { get; set; }
    }
}
