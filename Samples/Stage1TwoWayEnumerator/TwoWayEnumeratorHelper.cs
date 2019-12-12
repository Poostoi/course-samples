﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage1TwoWayEnumerator
{
    public static class TwoWayEnumeratorHelper
    {
        public static ITwoWayEnumerator<T> GetTwoWayEnumerator<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return new TwoWayEnumerator<T>(source.GetEnumerator());
        }
    }
}
