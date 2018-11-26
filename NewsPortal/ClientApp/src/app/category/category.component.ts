import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../services/category.service';
import { Category, News } from '../../services/types';
import { NewsService } from '../../services/news-service';
import { ActivatedRoute } from '@angular/router';
import { CategoryModel } from '../../../../Models/TypescriptModels/CategoryModel';
import { NewsModel } from '../../../../Models/TypescriptModels/NewsModel';

@Component({
    selector: 'app-category',
    templateUrl: './category.component.html',
    styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

    categories: CategoryModel[];
    news: NewsModel[];
    selected: number = 4;

    constructor(private categoryService: CategoryService, private newsService: NewsService, private activateRoute: ActivatedRoute) { }

    ngOnInit() {
        this.getCategories();
        //console.log(this.categories);

        this.activateRoute.params.subscribe(params => {
            if (params['id']) {
                this.selected = params['id'];
            } else {
                this.selected = -1;
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
        if (id == -1) {
            this.newsService.getNews().subscribe(res => { this.news = res; });
        } else {
            this.selected = id;

            this.newsService.getNewsByCategory(id).subscribe(res => { this.news = res; });
        }
    }

    getCategoryForId(id: number): string {
        if (id == -1) {
            return "Összes";
        }
        if (this.categories) {
            return this.categories.filter(_ => _.Id == id)[0].Title;
        }
        return "";
        
    }
}
