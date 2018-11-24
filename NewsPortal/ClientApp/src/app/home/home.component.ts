import { Component, OnInit } from '@angular/core';
import { NewsService } from "../../services/news-service"
import { News } from '../../services/types';
import { Router } from '@angular/router';
import { AuthGuard } from '../../services/auth-guard.service';

@Component({
  selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{

    news: News[];

    constructor(private newsService: NewsService, public router: Router, private auth: AuthGuard) { }

    ngOnInit() {
        this.getNews();
    }

    getNews(): void {
        this.news = this.newsService.getMockNews();
        // this.newsService.getMockNews().subscribe(news => this.news = news);
    }

    isLoggedIn() {
        return this.auth.getIsLoggedIn();
    }

    logOut() {
        this.auth.logOut();
    }

    getUserName() {
        return this.auth.getUserName();
    }
}
