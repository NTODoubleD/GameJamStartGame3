using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace DoubleDCore.UI
{
    public class CutOutMaskUI : Image
    {
        private static readonly int StencilComp = Shader.PropertyToID("_StencilComp");

        public override Material materialForRendering
        {
            get
            {
                Material forRendering = new Material(base.materialForRendering);
                forRendering.SetInt(StencilComp, (int)CompareFunction.NotEqual);
                return forRendering;
            }
        }
    }
}