import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../services/category.service';
import { Category, News } from '../../services/types';
import { NewsService } from '../../services/news-service';
import { ActivatedRoute } from '@angular/router';
import { CategoryModel } from '../../../../Models/TypescriptModels/CategoryModel';

@Component({
    selector: 'app-category',
    templateUrl: './category.component.html',
    styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

    categories: CategoryModel[];
    news: News[];
    selected: number = 4;

    constructor(private categoryService: CategoryService, private newsService: NewsService, private activateRoute: ActivatedRoute) { }

    ngOnInit() {
        this.getCategories();
        console.log(this.categories);

        this.activateRoute.params.subscribe(params => {
            if (params['id']) {
                this.selected = params['id'];
            } else {
                this.selected = 4;
            }

            this.loadCategory(this.selected);
        });
    }

    getCategories(): void {
        this.categoryService.getCategories().subscribe(result => {
            this.categories =  result;
        });
    }

    loadCategory(id: number): void {

        this.selected = id;

        this.news = this.newsService.getNewsByCategory(id);
    }

    getCategoryForId(id: number): string {
        return this.categoryService.getMockCategories().filter(_ => _.id == id)[0].title;
    }
}
