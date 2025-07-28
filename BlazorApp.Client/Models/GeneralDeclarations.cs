namespace BlazorApp.Models;


//<button @onclick = "Confirmed(0)" > Yes </ button >
//< button @onclick="Confirmed(1)">No</button>
//<button @onclick = "Confirmed(2)" > Delete </ button >
//< button @onclick="Confirmed(3)">Cancel</button>

[Flags]
public enum ModalButtonsEnum
{
	mbYes = 2,
	mbNo = 4,
	mbDelete = 8,
	mbCancel = 16
}
