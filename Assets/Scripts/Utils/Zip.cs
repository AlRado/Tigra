using System;
using Ionic.Zlib;

public static class Zip {
  public static string Compress (byte[] bytes) {
    return Convert.ToBase64String (ZlibStream.CompressBuffer (bytes));
  }

  public static byte[] Decompress (string data) {
    byte[] bytes = Convert.FromBase64String (data);
    return ZlibStream.UncompressBuffer (bytes);
  }

  // /// <summary>
  // /// Compress plain text to byte array
  // /// </summary>
  // public static byte[] Compress (string text) {
  //   return ZlibStream.CompressString (text);
  // }

  // /// <summary>
  // /// Compress plain text to compressed string
  // /// </summary>
  // public static string CompressToString (string text) {
  //   return Convert.ToBase64String (Compress (text));
  // }

  // /// <summary>
  // /// Decompress byte array to plain text
  // /// </summary>
  // public static string Decompress (byte[] bytes) {
  //   return Encoding.UTF8.GetString (ZlibStream.UncompressBuffer (bytes));
  // }

  // /// <summary>
  // /// Decompress compressed string to plain text
  // /// </summary>
  // public static string Decompress (string data) {
  //   return Decompress (Convert.FromBase64String (data));
  // }
}