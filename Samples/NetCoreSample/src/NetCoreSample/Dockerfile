FROM microsoft/dotnet

COPY . /src

RUN cd /src && dotnet restore

EXPOSE 5000

WORKDIR /src
CMD ["dotnet", "run"]
