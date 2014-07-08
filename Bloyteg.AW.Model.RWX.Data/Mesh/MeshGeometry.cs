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
using Bloyteg.AW.Model.RWX.Data.Components;
using Bloyteg.AW.Model.RWX.Data.Primitive;

namespace Bloyteg.AW.Model.RWX.Data.Mesh
{
    [KnownType(typeof(Block))]
    [KnownType(typeof(Cone))]
    [KnownType(typeof(Cylinder))]
    [KnownType(typeof(Disc))]
    [KnownType(typeof(Hemisphere))]
    [KnownType(typeof(Sphere))]
    [DataContract]
    public abstract class MeshGeometry : IGeometry
    {
        private readonly List<Clump> _children;
        private readonly List<Vertex> _vertices;
        private readonly List<Face> _faces;
        private readonly List<PrimitiveGeometry> _primitives;
        private readonly List<PrototypeInstance> _prototypeInstances;

        protected MeshGeometry()
        {
            _prototypeInstances = new List<PrototypeInstance>();
            _primitives = new List<PrimitiveGeometry>();
            _faces = new List<Face>();
            _vertices = new List<Vertex>();
            _children = new List<Clump>();
        }

        [DataMember]
        public ICollection<Clump> Children
        {
            get
            {
                return _children;
            }
        }

        [DataMember]
        public ICollection<PrimitiveGeometry> Primitives
        {
            get
            {
                return _primitives;
            }
        }

        [DataMember]
        public IList<Vertex> Vertices
        {
            get
            {
                return _vertices;
            }
        }

        [DataMember]
        public ICollection<Face> Faces
        {
            get
            {
                return _faces;
            }
        }

        [DataMember]
        public bool IsPrelit { get; set; }

        [DataMember]
        public IList<PrototypeInstance> PrototypeInstances
        {
            get { return _prototypeInstances; }
        }
    }
}
