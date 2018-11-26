import { Component, OnInit } from '@angular/core';
import '@ckeditor/ckeditor5-build-classic/build/translations/hu';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { NewsService } from '../../services/news-service';
import { News, Category } from '../../services/types';
import { CategoryService } from '../../services/category.service';
import { NewsModel } from '../../../../Models/TypescriptModels/NewsModel';
import { AuthGuard } from '../../services/auth-guard.service';
import { CategoryModel } from '../../../../Models/TypescriptModels/CategoryModel';


@Component({
    selector: 'app-news-editor',
    templateUrl: './news-editor.component.html',
    styleUrls: ['./news-editor.component.css']
})
export class NewsEditorComponent implements OnInit {

    public Editor = ClassicEditor;
    public config = {
        language: 'hu'
    };
    post: NewsModel;
    categories: CategoryModel[];

    public model: NewsModel = {
        Title: 'Title',
        Lead: 'Short description',
        CategoryIds: [],

        Content: '<p>Full description:</p><p>Text...</p>',
        Author: 'author',
        CreateDate: new Date(),
        ValidPeriod: 1,
        Id: 0
    };

    constructor(private newsService: NewsService, private categoryService: CategoryService, private auth: AuthGuard) { }

    ngOnInit() {
        this.getCategories();
    }
    

    getCategories() {
        this.categoryService.getCategories().subscribe(_ => this.categories = _);
    }

    savePost() {
        this.model.Author = this.auth.getUserName();

        this.newsService.savePost(this.model, 0);
    }
}
