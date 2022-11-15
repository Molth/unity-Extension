#region 作者

//===================================================||
//作者：溫柔可愛柠檬草  (b站)
//===================================================||

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RootLibrary;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Plugins.RootConfig
{
    public static class SceneControl
    {
        public static void LoadSceneByString(string mapName) //按名字打开场景
        {
            SceneManager.LoadScene(mapName);
        }

        public static void LoadSceneById(int id) //按Id打开场景
        {
            if (id >= SceneManager.sceneCountInBuildSettings) return;
            SceneManager.LoadScene(id);
        }

        public static void LoadLastScene() //打开上一个场景
        {
            if (SceneManager.GetActiveScene().buildIndex == 0) return;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        public static void LoadNextScene() //打开下一个场景
        {
            if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1) return;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public static class SoundControl
    {
        public static void ContinuePlay(this AudioSource t, List<AudioClip> list)
        {
            if (t.isPlaying || list.IsEmpty()) return;
            if (t.clip == null) t.clip = list[0];
            t.clip = t.clip.NextOne(list);
            t.Play();
        }

        public static void PlaySfx(this AudioSource t, List<AudioClip> list, string clipName)
        {
            foreach (var t1 in list.Where(t1 => t1.name.Equals(clipName)))
                t.PlayOneShot(t1);
        }

        public static void PlaySfx(this AudioSource t, List<AudioClip> list, string clipName, float volume)
        {
            foreach (var t1 in list.Where(t1 => t1.name.Equals(clipName)))
                t.PlayOneShot(t1, volume);
        }

        public static void PlaySfx(this AudioSource t, AudioClip clip)
        {
            t.PlayOneShot(clip);
        }

        public static void PlaySfx(this AudioSource t, AudioClip clip, float volume)
        {
            t.PlayOneShot(clip, volume);
        }

        public static bool PlaySfxIf(this AudioSource t, AudioClip clip, bool condition)
        {
            if (!condition) return false;
            t.PlayOneShot(clip);
            return false;
        }

        public static bool PlaySfxIf(this AudioSource t, AudioClip clip, bool condition, float volume)
        {
            if (!condition) return false;
            t.PlayOneShot(clip, volume);
            return false;
        }

        public static void StopPlay(this AudioSource t)
        {
            t.Stop();
            t.loop = false;
            t.clip = null;
        }

        public static void PlaySound(this AudioSource t, AudioClip clip)
        {
            t.clip = clip;
            t.Play();
        }

        public static void PlaySoundLoop(this AudioSource t, AudioClip clip)
        {
            t.clip = clip;
            t.loop = true;
            t.Play();
        }

        public static void PlaySound(this AudioSource t, AudioClip clip, float volume)
        {
            t.clip = clip;
            t.volume = volume;
            t.Play();
        }

        public static void PlaySoundLoop(this AudioSource t, AudioClip clip, float volume)
        {
            t.clip = clip;
            t.volume = volume;
            t.loop = true;
            t.Play();
        }
    }

    public static class LibV
    {
        #region Math

        public static T LastOne<T>(this T one, T[] list)
        {
            if (list.IsEmpty() || !one.In(list)) return default;
            var id = one.GetId(list);
            return id == 0 ? list[^1] : list[id - 1];
        }

        public static T LastOne<T>(this T one, List<T> list)
        {
            if (list.IsEmpty() || !one.In(list)) return default;
            var id = one.GetId(list);
            return id == 0 ? list[^1] : list[id - 1];
        }

        public static T NextOne<T>(this T one, List<T> list)
        {
            if (list.IsEmpty() || !one.In(list)) return default;
            var id = one.GetId(list);
            return id == list.Count - 1 ? list[0] : list[id + 1];
        }

        public static T NextOne<T>(this T one, T[] list)
        {
            if (list.IsEmpty() || !one.In(list)) return default;
            var id = one.GetId(list);
            return id == list.Length - 1 ? list[0] : list[id + 1];
        }

        public static float Sinerp(float start, float end, float value)
        {
            return MathV.Lerp(start, end, MathV.Sin(value * MathV.Pi * 0.5f));
        }

        public static float Coserp(float start, float end, float value)
        {
            return MathV.Lerp(start, end, 1.0f - MathV.Cos(value * MathV.Pi * 0.5f));
        }

        public static float CoSinLerp(float start, float end, float value)
        {
            return MathV.Lerp(start, end, value * value * (3.0f - 2.0f * value));
        }

        public static int NextPoNe()
        {
            if (MathV.RandomV.NextDouble() > 0.5) return -1;
            return 1;
        }

        public static List<double> LinSpaceByValue(double min, double end, double t)
        {
            if (t == 0 || end < min) return default;
            var temp = new List<double>();
            var cp = min;
            while (cp < end)
            {
                temp.Add(cp);
                cp += t;
            }

            return temp;
        }

        public static double[] LinSpaceByCount(double min, double end, int t)
        {
            var temp = new double[t];
            var delta = (end - min) / t;
            for (var i = 0; i < t; i++) temp[i] = min + delta * i;
            return temp;
        }

        #endregion

        #region Vector

        #region Vector2

        public static float RangeLerp(this Vector2 v, float t)
        {
            return MathV.Lerp(v.x, v.y, t);
        }

        public static float RangeInverseLerp(this Vector2 v, float value)
        {
            return MathV.InverseLerp(v.x, v.y, value);
        }

        public static float RangeRandom(this Vector2 v)
        {
            return MathV.Next(v.x, v.y);
        }

        public static float ComponentMin(this Vector2 v)
        {
            return MathV.Min(v.x, v.y);
        }

        public static float ComponentMax(this Vector2 v)
        {
            return MathV.Max(v.x, v.y);
        }

        public static Vector2 ComponentAbs(this Vector2 v)
        {
            return new Vector2(MathV.Abs(v.x), MathV.Abs(v.y));
        }

        public static Vector2 ComponentMultiply(this Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x * v2.x, v1.y * v2.y);
        }

        public static Vector2 Round(this Vector2 v)
        {
            return new Vector2(MathV.Round(v.x), MathV.Round(v.y));
        }

        public static Vector2 Round(this Vector2 v, int digits)
        {
            return new Vector2((float)Math.Round(v.x, digits), (float)Math.Round(v.y, digits));
        }

        public static Vector2Int RoundToInt(this Vector2 v)
        {
            return new Vector2Int(MathV.RoundToInt(v.x), MathV.RoundToInt(v.y));
        }

        public static float RangeLerp(this Vector2Int v, float t)
        {
            return MathV.Lerp(v.x, v.y, t);
        }

        public static float RangeInverseLerp(this Vector2Int v, float value)
        {
            return MathV.InverseLerp(v.x, v.y, value);
        }

        public static int RangeRandom(this Vector2Int v)
        {
            return MathV.Next(v.x, v.y);
        }

        public static int ComponentMax(this Vector2Int v)
        {
            return MathV.Max(v.x, v.y);
        }

        public static int ComponentMin(this Vector2Int v)
        {
            return MathV.Min(v.x, v.y);
        }

        public static Vector2Int ComponentAbs(this Vector2Int v)
        {
            return new Vector2Int(MathV.Abs(v.x), MathV.Abs(v.y));
        }

        public static Vector2Int ComponentMultiply(this Vector2Int v1, Vector2Int v2)
        {
            return new Vector2Int(v1.x * v2.x, v1.y * v2.y);
        }

        #endregion

        #region Vector3

        public static Vector3 EulerNormalize(this Vector3 angles)
        {
            for (var i = 0; i < 3; i++)
                if (angles[i] < -180) angles[i] = angles[i] + 360;
                else if (angles[i] > 180) angles[i] = angles[i] - 360;
            return angles;
        }

        public static float ComponentMax(this Vector3 v)
        {
            return MathV.Max(v.x, MathV.Max(v.y, v.z));
        }

        public static float ComponentMin(this Vector3 v)
        {
            return MathV.Min(v.x, MathV.Min(v.y, v.z));
        }

        public static Vector3 ComponentAbs(this Vector3 v)
        {
            return new Vector3(MathV.Abs(v.x), MathV.Abs(v.y), MathV.Abs(v.z));
        }

        public static Vector3 ComponentMultiply(this Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }

        public static Vector3 Round(this Vector3 v)
        {
            return new Vector3(MathV.Round(v.x), MathV.Round(v.y), MathV.Round(v.z));
        }

        public static Vector3 Round(this Vector3 v, int digits)
        {
            return new Vector3((float)Math.Round(v.x, digits), (float)Math.Round(v.y, digits),
                (float)Math.Round(v.z, digits));
        }

        public static Vector3Int RoundToInt(this Vector3 v)
        {
            return new Vector3Int(MathV.RoundToInt(v.x), MathV.RoundToInt(v.y), MathV.RoundToInt(v.z));
        }

        public static Color ToColor(this Vector3 v)
        {
            return new Color(v.x, v.y, v.z);
        }

        public static Color ToColor256(this Vector3 v)
        {
            return new Color(v.x / 255f, v.y / 255f, v.z / 255f);
        }

        public static Vector3Int EulerNormalize(this Vector3Int angles)
        {
            for (var i = 0; i < 3; i++)
                angles[i] = angles[i] switch
                {
                    < -180 => angles[i] + 360,
                    > 180 => angles[i] - 360,
                    _ => angles[i]
                };
            return angles;
        }

        public static int ComponentMax(this Vector3Int v)
        {
            return MathV.Max(v.x, MathV.Max(v.y, v.z));
        }

        public static int ComponentMin(this Vector3Int v)
        {
            return MathV.Min(v.x, MathV.Min(v.y, v.z));
        }

        public static Vector3Int ComponentAbs(this Vector3Int v)
        {
            return new Vector3Int(MathV.Abs(v.x), MathV.Abs(v.y), MathV.Abs(v.z));
        }

        public static Vector3Int ComponentMultiply(this Vector3Int v1, Vector3Int v2)
        {
            return new Vector3Int(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }

        #endregion

        #region Common

        public static float GetAngle2(this Vector2 start, Vector2 end)
        {
            var dir = (end - start).normalized;
            return MathV.Atan2(dir.y, dir.x) * 57.29578f;
        }

        public static float GetAngle3(this Vector3 start, Vector3 end)
        {
            var dir = (end - start).normalized;
            return MathV.Atan2(dir.y, dir.x) * 57.29578f;
        }

        public static Vector2 NextCircularPoint(this Vector2 center, float radius)
        {
            var num1 = MathV.Next(-radius, radius);
            var num2 = Math.Sqrt(radius * radius - num1 * num1);
            return new Vector2(center.x + num1, (float)(center.y + MathV.Next(-num2, num2)));
        }

        public static Vector2 NextSquarePoint(this Vector2 center, float square)
        {
            var num1 = MathV.Next(-square, square);
            var num2 = MathV.Next(-square, square);
            return new Vector2(center.x + num1, center.y + num2);
        }

        public static float Distance2(this Vector2 a, Vector2 b)
        {
            var num1 = (double)(a.x - b.x);
            var num2 = (double)(a.y - b.y);
            return (float)Math.Sqrt(num1 * num1 + num2 * num2);
        }

        public static float Distance3(this Vector3 a, Vector3 b)
        {
            var num1 = (double)(a.x - b.x);
            var num2 = (double)(a.y - b.y);
            var num3 = (double)(a.z - b.z);
            return (float)Math.Sqrt(num1 * num1 + num2 * num2 + num3 * num3);
        }

        public static Vector2 Middle2(this Vector2 a, Vector2 b)
        {
            return new Vector2((a.x + b.x) / 2, (a.y + b.y) / 2);
        }

        public static Vector2 Middle2(this Vector2[] t)
        {
            var length = t.Length;
            var num1 = 0f;
            var num2 = 0f;
            for (var i = 0; i < length; i++)
            {
                num1 += t[i].x;
                num2 += t[i].y;
            }

            return new Vector2(num1 / length, num2 / length);
        }

        public static Vector3 Middle3(this Vector3 a, Vector3 b)
        {
            return new Vector3((a.x + b.x) / 2, (a.y + b.y) / 2, (a.z + b.z) / 2);
        }

        public static Vector3 Middle3(this Vector3[] t)
        {
            var length = t.Length;
            var num1 = 0f;
            var num2 = 0f;
            var num3 = 0f;
            for (var i = 0; i < length; i++)
            {
                num1 += t[i].x;
                num2 += t[i].y;
                num3 += t[i].z;
            }

            return new Vector3(num1 / length, num2 / length, num3 / length);
        }

        public static Vector3 Bezier(Vector3 start, Vector3 mid, Vector3 end, float t)
        {
            return (1.0f - t) * (1.0f - t) * start + 2.0f * t * (1.0f - t) * mid + t * t * end;
        }

        public static Vector3 BezierMethod(float t, List<Vector3> forceList)
        {
            while (true)
            {
                if (forceList.Count < 2) return forceList[0];
                var temp = new List<Vector3>();
                for (var i = 0; i < forceList.Count - 1; i++)
                {
                    Debug.DrawLine(forceList[i], forceList[i + 1], Color.yellow);
                    var proportion = (1 - t) * forceList[i] + t * forceList[i + 1];
                    temp.Add(proportion);
                }

                forceList = temp;
            }
        }

        #endregion

        #endregion

        #region Delay

        public static IEnumerator IteratorDelay(float t, Action action) //协程延迟
        {
            yield return new WaitForSeconds(t);
            action();
        }

        public static async void AsyncDelay(float t, Action action) //异步延迟
        {
            await Task.Delay((int)(t * 1000));
            action?.Invoke();
        }

        /// <summary>
        ///     延迟
        /// </summary>
        public static void Delay(int t, Action action)
        {
            AsyncDelay(t, action);
        }

        /// <summary>
        ///     延迟
        /// </summary>
        public static void Delay(float t, Action action)
        {
            AsyncDelay(t, action);
        }

        /// <summary>
        ///     延迟
        /// </summary>
        public static void Delay(double t, Action action)
        {
            AsyncDelay((float)t, action);
        }

        /// <summary>
        ///     延迟
        /// </summary>
        public static void DelayFunc(int t, Action action)
        {
            AsyncDelay(t, action);
        }

        /// <summary>
        ///     延迟
        /// </summary>
        public static void DelayFunc(float t, Action action)
        {
            AsyncDelay(t, action);
        }

        /// <summary>
        ///     延迟
        /// </summary>
        public static void DelayFunc(double t, Action action)
        {
            AsyncDelay((float)t, action);
        }

        #endregion
    }
}