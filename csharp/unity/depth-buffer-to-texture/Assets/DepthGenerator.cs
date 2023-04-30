using System.IO;
using System.Collections;
using UnityEngine;

namespace PeasantsLogic
{
    /// <summary>
    /// The <c>DepthGenerator<c> stores depth data into [.exr] file.
    /// 
    /// The script should be attached to the Camera component because it 
    /// relies on the OnRenderImage function called by the Camera component.
    /// 
    /// The script depends on the Depth shader accessible under 
    /// Custom/Depth.
    /// 
    /// The texture size depends on the size of Camera output which could be
    /// the actual screen size as seen in the Game tab or it could be 
    /// RenderTexture set in the Target Texture property of the Camera 
    /// component.
    /// </summary>
    public class DepthGenerator : MonoBehaviour
    {
        [SerializeField]
        private bool record;

        [Space]
        [SerializeField]
        private string depthShaderName = "Custom/Depth";

        [SerializeField]
        private Material depthMaterial;

        [Space]
        [SerializeField]
        private int outputIndex;

        [SerializeField]
        private string outputPrefix = "Texture_";

        [SerializeField]
        private string outputDirectory = "Depth";

        [SerializeField]
        private RenderTexture outputRenderTexture;


        private void Start()
        {
            // We need to activate depth mode to enable camera to generate 
            // depth data for the custom shader to pick it up later during 
            // the rendering stage.
            Camera.main.depthTextureMode = DepthTextureMode.Depth;

            // Create the depth material which must contain depth shader
            // which is reponsible for picking up the depth data during the 
            // rendering stage.
            depthMaterial = new Material(Shader.Find(depthShaderName));

            // We make sure that the output folder exists and ready.
            Directory.CreateDirectory(outputDirectory);
        }

        private void Update()
        {
            // This is just a simple animation for demonstration purposes.
            // To be run only in Editor.
            if (Mathf.Floor(transform.position.x) == 5)
            {
                record = false;
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else
            {
                // For demonstration purposes we activate the recording.
                record = true;
                transform.Translate(Time.deltaTime, 0, 0, Space.Self);
            }
            Debug.Log(outputIndex);
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (record)
            {
                RenderTexture screenshot = GetRenderTexture(src);

                // The final output path for the new texture.
                string path =
                (
                    $"{outputDirectory}/{outputPrefix}{outputIndex++}.exr"
                );

                // Instruct the underlying renderer to render the graphics
                // onto the custom render texture using our custom shader.
                Graphics.Blit(src, screenshot, depthMaterial);

                // Save the results in the output folder in an async manner.
                SaveView(path, screenshot);

                // Record once.
                record = false;
            }

            // At the end of the day, optionally, we want to render the results
            // back to the output screen.
            Graphics.Blit(src, dest, depthMaterial);
        }

        // The source render texture is used to create an exact size 
        // destination render texture.
        private RenderTexture GetRenderTexture(RenderTexture src)
        {
            if (outputRenderTexture) // If it exists, then:
            {
                if // We want to adjust the size only if it is different.
                (
                    (outputRenderTexture.width != src.width) ||
                    (outputRenderTexture.height != src.height)
                )
                {
                    outputRenderTexture            = new RenderTexture
                    (
                        src.width                 ,
                        src.height                ,
                        32                        , // 32 bits for depth.
                        RenderTextureFormat.RFloat  // 32 bits in one channel.
                    );
                    outputRenderTexture.filterMode = FilterMode.Point;
                }
            }
            else // If it doesn't exist at all, then:
            {
                outputRenderTexture            = new RenderTexture
                (
                    src.width                 ,
                    src.height                ,
                    32                        , // 32 bits for depth.
                    RenderTextureFormat.RFloat  // 32 bits in one channel.
                );
                outputRenderTexture.filterMode = FilterMode.Point;
            }

            return outputRenderTexture;
        }

        private async void SaveView(string filePath, RenderTexture rt)
        {
            // Capturing.
            Texture2D texture = new Texture2D
            (
                width         : rt.width            ,
                height        : rt.height           ,
                textureFormat : TextureFormat.RFloat,
                mipChain      : false
            );
            texture.ReadPixels
            (
                source             : new Rect(0, 0, rt.width, rt.height),
                destX              : 0                                  ,
                destY              : 0                                  ,
                recalculateMipMaps : false
            );
            texture.Apply();

            // Saving to File System.
            // None          | No flag. This will result in an uncompressed
            //               | 16-bit float EXR file.
            // OutputAsFloat | The texture will be exported as a 32-bit float
            //               | EXR file (default is 16-bit).
            await File.WriteAllBytesAsync
            (
                path  : filePath                                             ,
                bytes : texture.EncodeToEXR(Texture2D.EXRFlags.OutputAsFloat)
            );

            // Free the resources.
            Destroy(texture);
        }
    } // DepthGenerator
} // PeasantsLogic
