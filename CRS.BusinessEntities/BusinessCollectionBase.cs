

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


    /// <summary>
    /// The BusinessCollectionBase class serves as the base class for all main business entity collections.
    ///
    public class BusinessCollectionBase<T> : Collection<T>
    {
        /// <summary>
        /// Initializes a new instance of the BusinessCollectionBase class.
        /// </summary>
        public BusinessCollectionBase() { }

        /// <summary>
        /// Initializes a new instance of the BusinessCollectionBase class and populates it with the initial list.
        /// </summary>
        public BusinessCollectionBase(IList<T> initialList) : base(initialList) { }


    }


