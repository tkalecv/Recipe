using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe.Helpers.AutoMapper
{
    public static class AutoMapperExtension
    {
        /// <summary>
        /// Method merges two objects into one
        /// </summary>
        /// <typeparam name="TResult">Destination object</typeparam>
        /// <param name="item1">First object you want to merge</param>
        /// <param name="item2">Second object you want to merge</param>
        /// <returns>TResult</returns>
        public static TResult MergeInto<TResult>(this IMapper mapper, object item1, object item2)
        {
            return mapper.Map(item2, mapper.Map<TResult>(item1));
        }

        /// <summary>
        /// Method merges multiple objects into one
        /// </summary>
        /// <typeparam name="TResult">Destination object</typeparam>
        /// <param name="objects">objects you want to merge</param>
        /// <returns>TResult</returns>
        public static TResult MergeInto<TResult>(this IMapper mapper, params object[] objects)
        {
            var res = mapper.Map<TResult>(objects.First());
            return objects.Skip(1).Aggregate(res, (r, obj) => mapper.Map(obj, r));
        }
    }
}
