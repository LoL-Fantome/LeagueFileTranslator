﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueFileTranslator.Structures;

namespace LeagueFileTranslator.FileTranslators.SCB.IO
{
    public class SCBFace
    {
        public uint[] Indices { get; private set; }
        public string Material { get; private set; }
        public Vector2[] UVs { get; private set; }

        public SCBFace(uint[] indices, string material, Vector2[] uvs)
        {
            this.Indices = indices;
            this.Material = material;
            this.UVs = uvs;
        }

        public SCBFace(BinaryReader br)
        {
            this.Indices = new uint[] { br.ReadUInt32(), br.ReadUInt32(), br.ReadUInt32() };
            this.Material = Encoding.ASCII.GetString(br.ReadBytes(64)).Replace("\0", "");

            float[] uvs = new float[] { br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle() };
            this.UVs = new Vector2[]
            {
                new Vector2(uvs[0], uvs[3]),
                new Vector2(uvs[1], uvs[4]),
                new Vector2(uvs[2], uvs[5])
            };
        }

        public void Write(BinaryWriter bw)
        {
            for (int i = 0; i < 3; i++)
            {
                bw.Write(this.Indices[i]);
            }

            bw.Write(this.Material.PadRight(64, '\u0000').ToCharArray());

            for (int i = 0; i < 3; i++)
            {
                bw.Write(this.UVs[i].X);
            }
            for (int i = 0; i < 3; i++)
            {
                bw.Write(this.UVs[i].Y);
            }
        }
    }
}
