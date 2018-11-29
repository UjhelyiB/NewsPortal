import { Component, OnInit } from '@angular/core';
import { News } from '../../services/types';
import { NewsService } from '../../services/news-service';
import { Router } from '@angular/router';
import { NewsModel } from '../../../../Models/TypescriptModels/NewsModel';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
    selector: 'app-search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

    news: NewsModel[];

    searchKeyWords: string = "";

    constructor(private newsService: NewsService, public router: Router) { }

    ngOnInit() {
        this.getNews();
    }

    getNews(): void {
        this.newsService.getNews().subscribe(res => { this.news = res; });
    }


    searchNews() {

        this.newsService.getNews().subscribe(res => {
            let keywords: string[] = this.searchKeyWords.split(' ');

            this.news = [];

            let result = new Set<NewsModel>();
            
                for (let news of res) {
                    let i = 0;
                    while (i < keywords.length) {
                        if (news.Author.indexOf(keywords[i]) == -1 && news.Content.indexOf(keywords[i]) == -1 && news.CreateDate.toString().indexOf(keywords[i]) == -1
                            && news.Lead.indexOf(keywords[i]) == -1 && news.Title.indexOf(keywords[i]) == -1) {
                            break;
                        }
                        i++;
                    }
                    if (i == keywords.length) {
                        result.add(news);
                    }
                }




            result.forEach(_ => this.news.push(_));
        });
    }

    onSideNavbarNeedsToBeReduced(isClosed: boolean) {
        this.newsService.getNews().subscribe(res => {
            this.searchNews();
        });
    }
}
