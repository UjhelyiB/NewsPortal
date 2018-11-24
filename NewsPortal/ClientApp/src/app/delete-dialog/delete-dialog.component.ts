import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material';

@Component({
    selector: 'app-delete-dialog',
    templateUrl: './delete-dialog.component.html'
})
export class DeleteDialogComponent {

    constructor(public dialogRef: MatDialogRef<DeleteDialogComponent>) {
    }

    onCancel(): void {
        this.dialogRef.close();
    }

    onOk() {
        this.dialogRef.close(true);
    }
}
