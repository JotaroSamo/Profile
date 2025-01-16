import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { delay } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  loadingBehaviour = new BehaviorSubject<boolean>(false);
  loadingMap: Map<string, boolean> = new Map<string, boolean>();

  get behavior(): Observable<boolean> {
    return this.loadingBehaviour.pipe(
      delay(0)
    );
  }

  setLoading(value: boolean, url: string): void {
    if (!url) {
      throw new Error('The request URL must be provided');
    }

    if (value) {
      this.loadingMap.set(url, value);
      this.loadingBehaviour.next(true);
    } else if (!value && this.loadingMap.has(url)) {
      this.loadingMap.delete(url);
    }

    if (this.loadingMap.size === 0) {
      this.loadingBehaviour.next(false);
    }
  }
}
