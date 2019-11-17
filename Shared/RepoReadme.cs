using System;
using Octokit;

namespace Shared {

    /// <summary>
    /// Readme Class
    /// </summary>
    public class RepoReadme {

        /// <summary>
        /// Reame Name
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// Readme Content
        /// </summary>
        /// <value></value>
        public string Content { get; set; }
    }
}
