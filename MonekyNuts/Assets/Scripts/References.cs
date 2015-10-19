using UnityEngine;
using System.Collections;

public static class References {

    public static StateManager stateManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<StateManager>();

}
