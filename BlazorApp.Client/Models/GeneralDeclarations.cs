namespace BlazorApp.Models;

[Flags]
public enum ModalButtonsEnum
{
	mbYes = 2,
	mbNo = 4,
	mbDelete = 8,
	mbCancel = 16
}
