import { Component, OnInit } from '@angular/core';
import { Category } from '../../services/types';
import { CategoryService } from '../../services/category.service';
import { MatDialog, MatDialogRef} from '@angular/material';
import { EditDialogComponent } from '../edit-dialog/edit-dialog.component';
import { DeleteDialogComponent } from '../delete-dialog/delete-dialog.component';
import { CategoryModel } from '../../../../Models/TypescriptModels/CategoryModel';

@Component({
    selector: 'app-category-editor',
    templateUrl: './category-editor.component.html',
    styleUrls: ['./category-editor.component.css']
})
export class CategoryEditorComponent implements OnInit {

    categories: CategoryModel[];
    displayedColumns: string[] = ['id', 'title', 'actions'];
    model: CategoryModel;

    constructor(private categoryService: CategoryService, public dialog: MatDialog) {
        this.model = {
            Id: 0,
            Title: '',
        }

    }

    ngOnInit() {
        this.getCategories();
    }


    getCategories(): void {
        this.categoryService.getCategories().subscribe(result => {
            this.categories = result;
        });
    };

    updateCategory(id: number, title: string): void {
        this.categoryService.updateCategory(id, { Id: id, Title: title }).subscribe(() => this.getCategories());
        //console.log('UPDATE CATEGORY: ' + id + ' TO: ' + title);
    }

    deleteCategory(id: number): void {
        this.categoryService.deleteCategory(id).subscribe(() => this.getCategories());
        //console.log('DELETE CATEGORI: ' + id);
    }

    createCategory(): void {
        this.categoryService.createCategory(this.model).subscribe(() => this.getCategories());
    }

    showEdit(id: number, title: string): void {
        const dialogRef = this.dialog.open(EditDialogComponent, {
            width: '350px',
            data: { Id: id, Title: title }
        });

        dialogRef.afterClosed().subscribe(result => {
            //console.log('The dialog was closed');
            if (result) {
                this.updateCategory(id, result);
            }
            
        });
    }

    showDelete(id: number): void {
        const dialogRef = this.dialog.open(DeleteDialogComponent, {
            width: '350px',
            data: { Id: id }
        });

        dialogRef.afterClosed().subscribe(result => {
            //console.log('The dialog was closed');
            if (result) {
                this.deleteCategory(id);
            }
        });
    }

    save() {
        //TODO
    }
}
