import { Component, OnInit } from '@angular/core';
import { NewsService } from "../../services/news-service"
import { News } from '../../services/types';
import { Router } from '@angular/router';
import { AuthGuard } from '../../services/auth-guard.service';
import { NewsModel } from '../../../../Models/TypescriptModels/NewsModel';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

    news: NewsModel[];

    constructor(private newsService: NewsService, public router: Router, private auth: AuthGuard) { }

    ngOnInit() {
        this.getNews();
    }

    getNews(): void {
        this.newsService.getNews().subscribe(res => {
            console.log(res);
            this.news = res;
        });
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
