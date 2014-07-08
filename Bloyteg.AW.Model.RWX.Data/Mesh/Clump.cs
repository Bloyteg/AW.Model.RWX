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
using System.Runtime.Serialization;
using Bloyteg.AW.Math;

namespace Bloyteg.AW.Model.RWX.Data.Mesh
{
    [DataContract(Name="Clump")]
    public class Clump : MeshGeometry, ITransformable
    {
        public Clump()
        {
            Transform = new Matrix4();
            IsCollidable = true;
        }

        [DataMember]
        public int? Tag { get; set; }

        [DataMember]
        public Matrix4 Transform { get; set; }

        [DataMember]
        public bool IsCollidable { get; set; }
    }
}
