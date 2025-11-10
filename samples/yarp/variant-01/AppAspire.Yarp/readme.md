# Setup


https://www.milanjovanovic.tech/blog/using-dotnet-aspire-with-the-docker-publisher

```bash
rm -fr $(find . -type d -iname obj -o -iname bin)
aspire run
```


```bash
rm -fr $(find . -type d -iname obj -o -iname bin)
aspire publish \
    --publisher docker-compose \
    --output artifacts-docker-compose
```

https://medium.com/@davidfowl/model-run-ship-the-new-way-to-build-distributed-apps-48d67286a665

