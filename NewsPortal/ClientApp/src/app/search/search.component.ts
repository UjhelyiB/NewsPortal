import { Component, OnInit } from '@angular/core';
import { News } from '../../services/types';
import { NewsService } from '../../services/news-service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

    news: News[];

    constructor(private newsService: NewsService, public router: Router) { }

    ngOnInit() {
        this.getNews();
    }

    getNews(): void {
        this.news = this.newsService.getMockNews();
        // this.newsService.getMockNews().subscribe(news => this.news = news);
    }
}
