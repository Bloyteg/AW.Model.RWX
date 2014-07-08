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
using System;
using System.Runtime.Serialization;
using Bloyteg.AW.Math;

namespace Bloyteg.AW.Model.RWX.Data.Components
{
    [DataContract]
    public enum GeometrySampling
    {
        [EnumMember]
        Solid,
        [EnumMember]
        Wireframe,
        [EnumMember]
        Pointcloud
    }

    [DataContract]
    public enum LightSampling
    {
        [EnumMember]
        Facet,
        [EnumMember]
        Vertex
    }

    [DataContract]
    [Flags]
    public enum TextureMode
    {
        [EnumMember]
        Null = 0x00,
        [EnumMember]
        Lit = 0x01,
        [EnumMember]
        Foreshorten = 0x02,
        [EnumMember]
        Filter = 0x04
    }

    [DataContract]
    public enum TextureAddressMode
    {
        [EnumMember]
        Wrap,
        [EnumMember]
        Mirror,
        [EnumMember]
        Clamp
    }

    [DataContract, Flags]
    public enum MaterialMode
    {
        [EnumMember]
        Null,
        [EnumMember]
        Double
    }

    [DataContract]
    public class Material : IEquatable<Material>
    {
        public Material()
        {
            Opacity = 1;
            TextureMode = TextureMode.Lit;
            Color = new Color();
        }

        [DataMember]
        public Color Color { get; private set; }

        [DataMember]
        public float Opacity { get; private set; }

        [DataMember]
        public float Ambient { get; private set; }

        [DataMember]
        public float Diffuse { get; private set; }

        [DataMember]
        public float Specular { get; private set; }

        [DataMember]
        public string Texture { get; private set; }

        [DataMember]
        public string Mask { get; private set; }

        [DataMember]
        public string Bump { get; private set; }

        [DataMember]
        public TextureMode TextureMode { get; private set; }

        [DataMember]
        public TextureAddressMode TextureAddressMode { get; private set; }

        [DataMember]
        public bool TextureMipmapState { get; private set; }

        [DataMember]
        public LightSampling LightSampling { get; private set; }

        [DataMember]
        public GeometrySampling GeometrySampling { get; private set; }

        [DataMember]
        public MaterialMode MaterialMode { get; private set; }

        private Material Copy()
        {
            return new Material
            {
                Color = new Color(Color.R, Color.G, Color.B),
                Opacity = Opacity,
                Ambient = Ambient,
                Diffuse = Diffuse,
                Texture = Texture,
                Mask = Mask,
                TextureMode = TextureMode,
                TextureAddressMode = TextureAddressMode,
                TextureMipmapState = TextureMipmapState,
                GeometrySampling = GeometrySampling,
                LightSampling = LightSampling,
                MaterialMode = MaterialMode
            };
        }

        public bool Equals(Material other)
        {
            return this.Equals(other, () => Color == other.Color &&
                                            Opacity == other.Opacity &&
                                            Ambient == other.Ambient &&
                                            Diffuse == other.Diffuse &&
                                            Texture == other.Texture &&
                                            Mask == other.Mask &&
                                            TextureMode == other.TextureMode &&
                                            TextureAddressMode == other.TextureAddressMode &&
                                            TextureMipmapState == other.TextureMipmapState &&
                                            GeometrySampling == other.GeometrySampling &&
                                            LightSampling == other.LightSampling &&
                                            MaterialMode == other.MaterialMode);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Material);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Ambient.GetHashCode() * Diffuse.GetHashCode() + (Texture ?? string.Empty).GetHashCode();
            }
        }

        public static bool operator ==(Material lhs, Material rhs)
        {
            return lhs.IsEqualTo(rhs);
        }

        public static bool operator !=(Material lhs, Material rhs)
        {
            return !lhs.IsEqualTo(rhs);
        }

        public Material UpdateTexture(string texture, string mask, string bump)
        {
            var copy = Copy();
            copy.Texture = texture;
            copy.Mask = mask;
            copy.Bump = bump;
            return copy;
        }

        public Material UpdateColor(float r, float g, float b)
        {
            var copy = Copy();
            copy.Color = new Color(r, g, b);
            return copy;
        }

        public Material UpdateSurface(float ambient, float diffuse, float specular)
        {
            var copy = Copy();
            copy.Ambient = ambient;
            copy.Diffuse = diffuse;
            copy.Specular = specular;
            return copy;
        }

        public Material UpdateAmbient(float ambient)
        {
            var copy = Copy();
            copy.Ambient = ambient;
            return copy;
        }

        public Material UpdateDiffuse(float diffuse)
        {
            var copy = Copy();
            copy.Diffuse = diffuse;
            return copy;
        }

        public Material UpdateSpecular(float specular)
        {
            var copy = Copy();
            copy.Specular = specular;
            return copy;
        }

        public Material UpdateOpacity(float opacity)
        {
            var copy = Copy();
            copy.Opacity = opacity;
            return copy;
        }

        public Material UpdateTextureMode(TextureMode textureMode)
        {
            var copy = Copy();
            copy.TextureMode = textureMode;
            return copy;
        }

        public Material AddTextureMode(TextureMode textureMode)
        {
            var copy = Copy();
            copy.TextureMode |= textureMode;
            return copy;
        }

        public Material RemoveTextureMode(TextureMode textureMode)
        {
            var copy = Copy();
            copy.TextureMode &= ~textureMode;
            return copy;
        }

        public Material UpdateMaterialMode(MaterialMode materialMode)
        {
            var copy = Copy();
            copy.MaterialMode = materialMode;
            return copy;
        }

        public Material AddMaterialMode(MaterialMode materialMode)
        {
            var copy = Copy();
            copy.MaterialMode |= materialMode;
            return copy;
        }

        public Material RemoveMaterialMode(MaterialMode materialMode)
        {
            var copy = Copy();
            copy.MaterialMode &= ~materialMode;
            return copy;
        }

        public Material UpdateGeometrySampling(GeometrySampling sampling)
        {
            var copy = Copy();
            copy.GeometrySampling = sampling;
            return copy;
        }

        public Material UpdateLightSampling(LightSampling sampling)
        {
            var copy = Copy();
            copy.LightSampling = sampling;
            return copy;
        }

        public Material UpdateTextureAddressMode(TextureAddressMode mode)
        {
            var copy = Copy();
            copy.TextureAddressMode = mode;
            return copy;
        }

        public Material UpdateTextureMipmapState(bool state)
        {
            var copy = Copy();
            copy.TextureMipmapState = state;
            return copy;
        }
    }
}
