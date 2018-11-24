import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Category } from '../../services/types';

@Component({
    selector: 'app-edit-dialog',
    templateUrl: './edit-dialog.component.html'
})
export class EditDialogComponent {

    constructor(public dialogRef: MatDialogRef<EditDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: Category) {
    }

    onCancel(): void {
        this.dialogRef.close();
    }

    onOk() {
        this.dialogRef.close(this.data.title);
    }
}
