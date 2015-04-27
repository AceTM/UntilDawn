using UnityEngine;
using System.Collections;

public struct DialogMessage {
	public int 		Id { get; set; }
	public string 	Character { get; set; }
	public bool 	Skipable { get; set; }
	public bool 	Voiced { get; set; }
	public bool 	Left { get; set; }
	public string 	JpText { get; set; }
	public string 	EnText { get; set; }
}