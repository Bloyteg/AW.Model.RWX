// Copyright 2014 Joshua R. Rodgers
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Bloyteg.AW.Model.RWX.Data.Components
{
    [DataContract]
    public class Face
    {
        public Face()
        {
            
        }

        public Face(int index0, int index1, int index2)
        {
            Indices = new[] {index0, index1, index2};

            Triangles = new List<Triangle>
                            {
                                new Triangle(index0, index1, index2)
                            };
        }

        [DataMember]
        public int MaterialId { get; set; }

        [DataMember]
        public int? Tag { get; set; }

        [DataMember]
        public IEnumerable<int> Indices { get; set; }

        [DataMember]
        public ICollection<Triangle> Triangles { get; set; }
    }
}
