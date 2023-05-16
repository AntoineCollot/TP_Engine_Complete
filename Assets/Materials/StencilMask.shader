// "Invisible" Unity Occlusion Shader. Useful for AR, Masking, etc
// Mark Johns / Doomlaser - https://twitter.com/Doomlaser

Shader "Customs/StencilMask"
{
    Properties
    {
    }
    SubShader
    {
	
		
        Tags {"Queue" = "Geometry-10" }
		Stencil
        {
            Ref 1
            Comp Always
            Pass Replace
        }
         ColorMask 0
         ZWrite Off
         Pass {}
    }
}