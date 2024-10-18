using System.Collections.Generic;

public static class ResultScoreManager
{
    public static int currentScore; // �Q�[���I�����̃X�R�A��ێ�����ÓI�ϐ�
    public static List<int> highScores; // ���s���̃����L���O��ێ����郊�X�g
    static bool initialized = false; // �������ς݂��ǂ����̃t���O

    // �����L���O���X�g�����������郁�\�b�h
    public static void Initialize()
    {
        if (!initialized)
        {
            highScores = new List<int>();
            initialized = true;
        }
    }
}