import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NewsService } from '../../services/news-service';
import { NewsModel } from '../../../../Models/TypescriptModels/NewsModel';

@Component({
    selector: 'app-fetch-data',
    templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {

    private news: NewsModel[];

    constructor(private newsService: NewsService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    }

    ngOnInit(): void {
        this.newsService.getNews().subscribe(res => {
            this.news = res;
        });
    }
}
