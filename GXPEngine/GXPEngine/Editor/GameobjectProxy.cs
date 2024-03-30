﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;

namespace GXPEngine.Editor
{
    public class GameobjectProxy : DSCFSprite
    {
        public ConstructorInfo Constructor;
        public Type ObjectType;
        float radius = .25f;
        public GameobjectProxy() : base("editor/ProxyLogo.png")
        {
            SetOrigin(width * .5f,height * .5f);
        }

        public override void RenderDepthSorted(GLContext glContext, Vector3 slop)
        {
            Vector3 baseScale = scaleXYZ;
            scaleXYZ = baseScale*(16.0f/game.width);
            if (((Editor)game).selectedGameobject == this)
                slop.z = .1f;
            base.RenderDepthSorted(glContext, slop);
            scaleXYZ = baseScale;
        }
        protected override Collider createCollider()
        {
            return new BoxCollider3D(this);
        }
        public override Vector3[] GetExtents()
        {
            Vector3[] res = new Vector3[8];
            for (int i = 0; i < 8; i++)
                res[i] = TransformPoint((i & 1) != 0 ? -radius : radius, (i & 2) != 0 ? -radius : radius, (i & 4) != 0 ? -radius : radius);
            return res;
        }
    }
}
