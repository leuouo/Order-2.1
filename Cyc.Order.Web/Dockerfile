#docker 编译文件

#基础镜像
FROM microsoft/dotnet:2.1-aspnetcore-runtime
#定义参数
ARG source=.
#设置工作目录
WORKDIR /wwwroot
#暴露端口
EXPOSE 8802
#把所有文件复制到工作目录
COPY $source .
#设置本地时间
RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai  /etc/localtime

ENV ASPNETCORE_URLS http://*:8802
#程序入口 
ENTRYPOINT ["dotnet", "Cyc.Order.Web.dll"]