import { Component, OnInit } from '@angular/core';
import { NewsService } from "../../services/news-service"
import { News } from '../../services/types';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{

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
