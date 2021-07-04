﻿using System.Text.Json.Serialization;
using <%= projectNamespace %>.Utils.Entities;

namespace <%= projectNamespace %>.Web.Common.Dto
{
    public sealed class OrderApiDto
    {
        /// <summary>
        /// Field to order by
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Order direction
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SortOrder? Direction { get; set; }
    }
}
