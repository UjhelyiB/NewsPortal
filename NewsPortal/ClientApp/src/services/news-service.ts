import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { BehaviorSubject, Observable } from "rxjs";
import { NewsModel } from "../../../Models/TypescriptModels/NewsModel";
import { News } from "./types";
import { NEWS } from "./mock-news"
import { Set } from "../../node_modules/core-js";

@Injectable()
export class NewsService {

    private _news: BehaviorSubject<NewsModel[]>;
    public mycreatedpost;

    private httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json'
        })
    };

    public postmodel: NewsModel = {
        Title: 'Mock Title',
        Lead: 'Indonesian police have arrested a man over the murder of an elderly Japanese couple whose bodies were found burned beyond recognition on the holiday island of Bali.',
        CategoryIds: [0, 1],
        Content: '<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>',
        Author: 'DodoJoy',
        CreateDate: new Date(),
        Id: 0,
        ValidPeriod: 1
    };

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {


    }

     getNews(): Observable<NewsModel[]> {
         var x = this.http.get<NewsModel[]>(this.baseUrl + 'api/News/GetAllNews');

         return x;
    }

    getNewsById(id: number): Observable<NewsModel> {
        var x = this.http.get<NewsModel>(`${this.baseUrl}api/News/GetNews/${id}`);

        return x;
    }


    getNewsByCategory(id: number): Observable<NewsModel[]> {
        return this.http.get<NewsModel[]>(this.baseUrl + 'api/News/GetNewsByCategory/?category=' + id);

        //return NEWS.filter(cat => cat.CategoryIds.filter(catId => catId == id).length > 0);
    }

    
    savePost(post: NewsModel, id?: number) {
        this.http.post<void>(this.baseUrl + 'api/News/', { model: post}, this.httpOptions)
            .subscribe(result => {
                //if ((result as UserModel).UserID != undefined) {
                //    this.setUser(result as UserModel);

                //}
            });

        this.mycreatedpost = post;
    }

    getMyMockedPost() {
        return this.postmodel;
    }
}
