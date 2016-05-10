using UnityEngine;
using System.Collections;

public enum _BLOCK_TYPE
{
    _VERTICAL=0,
    _HORIZONTAL,
    BOTH
}
public class ClassDefine {

    public const int _SIZE_W = 110;
    public const int _SIZE_H = 110;

    public static readonly int[] _SIZE_X_LIST = new int[6] { -330, -220, -110, 0, 110, 220 };
    public static readonly int[] _SIZE_Y_LIST = new int[6] { 330, 220, 110, 0, -110, -220 };

}
