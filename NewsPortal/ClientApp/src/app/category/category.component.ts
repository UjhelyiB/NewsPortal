import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../services/category.service';
import { Category, News } from '../../services/types';
import { NewsService } from '../../services/news-service';

@Component({
    selector: 'app-category',
    templateUrl: './category.component.html',
    styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

    categories: Category[];
    news: News[];
    selected: 0;

    constructor(private categoryService: CategoryService, private newsService: NewsService) { }

    ngOnInit() {
        this.getCategories();
        this.loadCategory(this.selected);
    }

    getCategories(): void {
        this.categories = this.categoryService.getMockCategories();
    }

    loadCategory(id: number): void {
        this.news = this.newsService.getNewsByCategory(id);
        console.log('selected category: ' + id)
    }
}
