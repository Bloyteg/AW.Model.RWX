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
using System.Linq;
using System.Runtime.Serialization;
using Bloyteg.AW.Model.RWX.Data.Components;
using Bloyteg.AW.Model.RWX.Data.Mesh;

namespace Bloyteg.AW.Model.RWX.Data
{
    [DataContract]
    public enum AxisAlignment
    {
        [EnumMember]
        None,
        [EnumMember]
        ZOrientX,
        [EnumMember]
        ZOrientY,
        [EnumMember]
        XYZ
    }

    [DataContract]
    public class Model
    {
        public Model()
        {
            Materials = new List<Material>();
            Prototypes = new List<Prototype>();
        }

        [DataMember]
        public IList<Prototype> Prototypes { get; private set; }

        [DataMember]
        public IList<Material> Materials { get; private set; }

        [DataMember(Name="Clump")]
        public Clump MainClump
        {
            get; set;
        }

        [DataMember]
        public bool HasOpacityFix { get; set; }

        [DataMember]
        public bool HasRandomUVs { get; set; }

        [DataMember]
        public bool IsSeamless { get; set; }

        [DataMember]
        public AxisAlignment AxisAlignment { get; set; }

        public int AddMaterial(Material material)
        {
            foreach (var currentMaterial in Materials
                .Select((currentMaterial, index) => new { Material = currentMaterial, Index = index })
                .Where(currentMaterial => currentMaterial.Material.GetHashCode() == material.GetHashCode() && currentMaterial.Material == material))
            {
                return currentMaterial.Index;
            }

            Materials.Add(material);
            return Materials.Count - 1;
        }
    }
}
