﻿// Copyright 2014 Joshua R. Rodgers
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
using System;
using System.Collections.Generic;
using System.Linq;
using Bloyteg.AW.Math;
using Bloyteg.AW.Math.Geometry;
using Bloyteg.AW.Model.RWX.Data.Components;

namespace Bloyteg.AW.Model.RWX.Data.Builder
{
    public partial class ModelBuilder
    {
        private void PrevalidateGeometry()
        {
            //Handle there being no mesh to add to.
            if (_currentMeshGeometry == null)
            {
                throw new InvalidOperationException("This command is only valid inside a prototype or clump.");
            }
        }

        public void AddPolygon(int count, IEnumerable<int> indices, int? tag)
        {
            PrevalidateGeometry();

            //Error out on polygons not matching, for the sake of being consistent with AW and other RWX loaders.
            if(indices.Count() != count)
            {
                throw new InvalidOperationException("Polygon vertex count mismatch.");
            }

            _currentMeshGeometry.Faces.Add(new Face
                                               {
                                                   Indices = indices.ToList(),
                                                   MaterialId = _model.AddMaterial(_currentMaterial),
                                                   Tag = tag,
                                                   Triangles = TessellatePolygon(indices).ToList()
                                               });

            if(_currentMaterial.MaterialMode == MaterialMode.Double)
            {
                _currentMeshGeometry.Faces.Add(new Face
                {
                    Indices = indices.Reverse().ToList(),
                    MaterialId = _model.AddMaterial(_currentMaterial),
                    Tag = tag,
                    Triangles = TessellatePolygon(indices.Reverse()).ToList()
                });
            }
        }


        public void AddQuad(int index0, int index1, int index2, int index3, int? tag)
        {
            PrevalidateGeometry();

            var indicesAsArray = new[]
            {
                index0,
                index1,
                index2,
                index3
            };

            _currentMeshGeometry.Faces.Add(new Face
            {
                Indices = indicesAsArray,
                MaterialId = _model.AddMaterial(_currentMaterial),
                Tag = tag,
                Triangles = TessellatePolygon(indicesAsArray).ToList()
            });

            if (_currentMaterial.MaterialMode == MaterialMode.Double)
            {
                _currentMeshGeometry.Faces.Add(new Face
                {
                    Indices = indicesAsArray.Reverse().ToList(),
                    MaterialId = _model.AddMaterial(_currentMaterial),
                    Tag = tag,
                    Triangles = TessellatePolygon(indicesAsArray.Reverse()).ToList()
                });
            }
        }

        public void AddTriangle(int index0, int index1, int index2, int? tag)
        {
            PrevalidateGeometry();

            var triangle = new Face(index0, index1, index2)
                               {
                                   MaterialId = _model.AddMaterial(_currentMaterial),
                                   Tag = tag
                               };
           
            _currentMeshGeometry.Faces.Add(triangle);

            if(_currentMaterial.MaterialMode == MaterialMode.Double)
            {
                var backTriangle = new Face(index0, index1, index2)
                {
                    MaterialId = _model.AddMaterial(_currentMaterial),
                    Tag = tag
                };

                _currentMeshGeometry.Faces.Add(backTriangle);
            }
        }

        public void AddVertex(Tuple<double, double, double> position, Tuple<float, float> uv, Tuple<float, float, float> prelight)
        {
            PrevalidateGeometry();

            var vertex = new Vertex
            {
                Position = new Vector3(position.Item1, position.Item2, position.Item3)
            };

            vertex.Position *= _currentTransform;

            //Handle the UV
            if (uv != null)
            {
                vertex.UV = new UV(uv.Item1, uv.Item2);
            }

            //Handle prelight
            if(prelight != null)
            {
                _currentMeshGeometry.IsPrelit = true;
                _currentPrelight = new Color(prelight.Item1, prelight.Item2, prelight.Item3);
            }

            vertex.Prelight = _currentPrelight;

            _currentMeshGeometry.Vertices.Add(vertex);
        }

        /// <summary>
        /// Processes the indices to triangle.
        /// </summary>
        /// <param name="indices">The indices.</param>
        private IEnumerable<Triangle> TessellatePolygon(IEnumerable<int> indices)
        {
            var triangulator = new Triangulator(_currentMeshGeometry.Vertices.Select(vertex => vertex.Position).ToList(), indices);

            return triangulator.GetTriangles().Select(triangle => new Triangle(triangle.Item1, triangle.Item2, triangle.Item3));
        }
    }
}
