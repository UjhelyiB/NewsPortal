import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BehaviorSubject } from "rxjs";
import { NewsModel } from "../../../Models/TypescriptModels/NewsModel";

@Injectable()
export class NewsService {
    private _news: BehaviorSubject<NewsModel[]>;

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
}
