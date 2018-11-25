import { Component, OnInit } from '@angular/core';
import '@ckeditor/ckeditor5-build-classic/build/translations/hu';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { NewsService } from '../../services/news-service';
import { News, Category } from '../../services/types';
import { CategoryService } from '../../services/category.service';
import { NewsModel } from '../../../../Models/TypescriptModels/NewsModel';
import { AuthGuard } from '../../services/auth-guard.service';


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
    post: News;
    categories: Category[];

    public model: NewsModel = {
        Title: 'Mock Title',
        Lead: 'Indonesian police have arrested a man over the murder of an elderly Japanese couple whose bodies were found burned beyond recognition on the holiday island of Bali.',
        CategoryIds: [0, 1],
        Content: '<p>Hello world!</p>',
        Author: 'author',
        CreateDate: new Date(),
        ValidPeriod: 1,
        Id: 0
    };

    constructor(private newsService: NewsService, private categoryService: CategoryService, private auth: AuthGuard) { }

    ngOnInit() {
        this.getPost(0);
        this.getCategories();
    }

    getPost(id: number) {
        this.post = this.newsService.getMockPost(id);
    }

    getCategories() {
        this.categories = this.categoryService.getMockCategories();
    }

    savePost() {
        this.model.Author = this.auth.getUserName();

        this.newsService.savePost(this.model, 0);
    }
}
