import { Component, OnInit } from '@angular/core';
import { Category } from '../../services/types';
import { CategoryService } from '../../services/category.service';
import { MatDialog, MatDialogRef} from '@angular/material';
import { EditDialogComponent } from '../edit-dialog/edit-dialog.component';
import { DeleteDialogComponent } from '../delete-dialog/delete-dialog.component';

@Component({
    selector: 'app-category-editor',
    templateUrl: './category-editor.component.html',
    styleUrls: ['./category-editor.component.css']
})
export class CategoryEditorComponent implements OnInit {

    categories: Category[];
    displayedColumns: string[] = ['id', 'title', 'actions'];

    constructor(private categoryService: CategoryService, public dialog: MatDialog) { }

    ngOnInit() {
        this.getCategories();
    }

    getCategories(): void {
        this.categories = this.categoryService.getMockCategories();
    }

    updateCategory(id: number, title: string): void {
        // TODO
        console.log('UPDATE CATEGORY: ' + id + ' TO: ' + title);
    }

    deleteCategory(id: number): void {
        // TODO
        console.log('DELETE CATEGORI: ' + id);
    }

    showEdit(id: number, title: string): void {
        const dialogRef = this.dialog.open(EditDialogComponent, {
            width: '350px',
            data: { id: id, title: title }
        });

        dialogRef.afterClosed().subscribe(result => {
            console.log('The dialog was closed');
            if (result) {
                this.updateCategory(id, result);
            }
            
        });
    }

    showDelete(id: number): void {
        const dialogRef = this.dialog.open(DeleteDialogComponent, {
            width: '350px',
            data: { id: id }
        });

        dialogRef.afterClosed().subscribe(result => {
            console.log('The dialog was closed');
            if (result) {
                this.deleteCategory(id);
            }
        });
    }
}
