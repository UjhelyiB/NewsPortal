import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BehaviorSubject } from "rxjs";
import { NewsModel } from "../../../Models/TypescriptModels/NewsModel";
import { News } from "./types";
import { NEWS } from "./mock-news"

@Injectable()
export class NewsService {
    private _news: BehaviorSubject<NewsModel[]>;
    public mycreatedpost;

    public postmodel = {
        title: 'Mock Title',
        lead: 'Indonesian police have arrested a man over the murder of an elderly Japanese couple whose bodies were found burned beyond recognition on the holiday island of Bali.',
        categories: [0, 1],
        editorData: '<p>Hello world!</p>',
        author: 'DodoJoy',
        createDate: '2018.11.24',
    };

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
        this._news = new BehaviorSubject<NewsModel[]>(null);


        this.http.get<NewsModel[]>(this.baseUrl + 'api/News/GetAllNews').subscribe(result => {

            this.setNews(result);
        }, error => console.error(error));

    }

    setNews(value: NewsModel[]) {
        this._news.next(value);
    }

    getNews() {
        return this._news.asObservable();
    }

    getMockNews(): News[] {
        return NEWS;
    }

    getMockPost(id: number): News {
        return NEWS[0];
    }

    getNewsByCategory(id: number): News[] {
        return NEWS;
    }

    
    savePost(post: any, id ?: number) {
        this.mycreatedpost = post;
    }

    getMyMockedPost() {
        if (this.mycreatedpost) {
            return this.mycreatedpost;
        } else {
            return this.postmodel;
        } 
    }
}
