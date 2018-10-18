using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePause {

	private static bool pause = false;

    /// <summary> Uruchamia tryb pauzy w grze </summary>
    public static void Pause() {
        pause = true;
    }

    /// <summary> Wyłącza tryb pauzy w grze </summary>
    public static void Resume() {
        pause = false;
    }

    /// <summary> Pobieranie informacji o stanie pauzy w grze </summary>
    /// <returns> Pauza </returns>
    public static bool IsPause() {
        return pause;
    }

}
