FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine3.14-amd64

WORKDIR /app
COPY ./publish .

#ENV ASPNETCORE_URLS=http://0.0.0.0:3000

RUN apk add --no-cache \
        --repository http://dl-3.alpinelinux.org/alpine/edge/testing/ --allow-untrusted \
        libgdiplus \
        wkhtmltopdf \
        xvfb \
        ttf-dejavu ttf-droid ttf-freefont ttf-liberation \
    ;

RUN apk add --no-cache msttcorefonts-installer fontconfig && \
        update-ms-fonts && \
        fc-cache -f \
    ;

RUN mkdir /app/Rotativa;
RUN ln -s /usr/bin/wkhtmltopdf /app/Rotativa/wkhtmltopdf;
RUN chmod +x /app/Rotativa/wkhtmltopdf;

#ENTRYPOINT ["dotnet", "CertificatesSystem.WebUI.dll"]

CMD ASPNETCORE_URLS=http://*:$PORT dotnet CertificatesSystem.WebUI.dll