<?xml version="1.0"?>
<xsl:stylesheet  version="1.0" 
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns="http://www.w3.org/2000/svg"
xmlns:svg="http://www.w3.org/2000/svg"
xmlns:xlink="http://www.w3.org/1999/xlink"
 xmlns:tb="http://www.treebuilder.de/SVGEXT" >
<xsl:param name="currentTime" select="1"/>

	<xsl:template match="/">
			<xsl:apply-templates />
	</xsl:template>
	<xsl:template match="svg:*">
		<xsl:copy>
				  <xsl:apply-templates select="@*|node()|text"/>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="@*">
		<xsl:copy/>
	</xsl:template>
		<xsl:template match="processing-instruction()">
		<xsl:copy/>
	</xsl:template>
		
		<xsl:template match="svg:animate"/>
 <xsl:template match="svg:Transform"/>
<xsl:template match="@transform[parent::svg:*/child::svg:animateTransform]">
	<xsl:variable name="nodeName" select="name()"/>
	<xsl:variable name="nodeValue" select="."/>
	<xsl:for-each select="parent::svg:*/child::svg:animateTransform">
		<xsl:variable name="dur" >
			<xsl:call-template name="simpleTime">
			  <xsl:with-param name="val" select="@dur"/>
			</xsl:call-template>
		</xsl:variable>
	   <xsl:variable name="begin0" >
			<xsl:call-template name="simpleTime">
			  <xsl:with-param name="val" select="@begin"/>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="begin" >
		<xsl:choose>
		  <xsl:when test="@repeatCount='indefinite' and $currentTime &gt;= $begin0 ">
			<xsl:value-of select="$begin0 + $dur * floor(($currentTime - $begin0) div $dur)"/>
		  </xsl:when>
		  <xsl:when test="(@repeatCount*$dur  + $begin0)  &gt;=  ($currentTime) and ($currentTime &gt;= $begin0)">
			<xsl:value-of select="$begin0 + $dur * floor(($currentTime - $begin0) div $dur)"/>
		  </xsl:when>
		  <xsl:otherwise>
		  <xsl:value-of select="$begin0"/>
		  </xsl:otherwise>
		</xsl:choose>
		</xsl:variable>
		<xsl:variable name="from" select="@from"/><!-- delete! use @from everywhere, instead -->
		<xsl:variable name="to" select="@to"/>
		<xsl:choose>
			<xsl:when test="$begin &lt;= $currentTime and ($begin + $dur) &gt;= $currentTime">
				<xsl:choose>
				  <xsl:when test="@values">
				  <xsl:variable name="x" select="string-length(@values)"/>
				  <xsl:variable name="y" select="string-length(translate(@values,';',''))"/>
				  <xsl:variable name="z" select="$x - $y"/>
				  <xsl:variable name="mult" select="$dur div ($z)"/>
				  <xsl:variable name="index" select="floor(($currentTime - $begin) div $mult)"/>
				  <!--xsl:attribute name="debug">
				  <xsl:value-of select="$index"/>/<xsl:value-of select="$mult"/>
				  </xsl:attribute-->
					<xsl:variable name="value1">
						<xsl:call-template name="valueList">
						  <xsl:with-param name="liste" select="@values"/>
						  <xsl:with-param name="i" select="$index"/>
						</xsl:call-template>
					</xsl:variable>
					<xsl:variable name="value2">
						<xsl:call-template name="valueList">
						  <xsl:with-param name="liste" select="@values"/>
						  <xsl:with-param name="i" select="$index + 1"/>
						</xsl:call-template>
					</xsl:variable>
					<xsl:variable name="iv">
					<xsl:call-template name="interpolateList">
					  <xsl:with-param name="liste1" select="$value1"/>
					  <xsl:with-param name="liste2" select="$value2"/>
					  <xsl:with-param name="p" select="($currentTime - ($begin + $index * $mult)) div $mult"/>
					</xsl:call-template>
					</xsl:variable>
					<xsl:attribute name="{$nodeName}">
					  <xsl:value-of select="@type"/>(<xsl:value-of select="$iv"/>)
					</xsl:attribute>
				  </xsl:when>
				  <xsl:otherwise>
				  <xsl:variable name="iv">
					<xsl:call-template name="interpolateList">
					  <xsl:with-param name="liste1" select="@from"/>
					  <xsl:with-param name="liste2" select="@to"/>
					  <xsl:with-param name="p" select="($currentTime - $begin) div $dur"/>
					</xsl:call-template>
					</xsl:variable>
					<xsl:attribute name="{$nodeName}"><xsl:value-of select="@type"/>(<xsl:value-of select="$iv"/>)</xsl:attribute>
				  </xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="($begin + $dur) &lt; $currentTime and @fill='freeze'">
			
				<xsl:attribute name="{$nodeName}"><xsl:value-of select="@type"/>(<xsl:value-of select="$to"/>)</xsl:attribute>
			</xsl:when>
			<xsl:otherwise>
				<xsl:copy />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:for-each>
</xsl:template>     

<xsl:template name="interpolateList">
<xsl:param name="liste1"/>
<xsl:param name="liste2"/>
<xsl:param name="p"/>
<xsl:choose>
<xsl:when test="contains($liste1,',')">
<xsl:variable name="v1" select="substring-before($liste1,',')"/>
<xsl:variable name="v2" select="substring-before($liste2,',')"/>
<xsl:value-of select="$v1 + ($v2 - $v1)*$p"/>,
<xsl:call-template name="interpolateList">
<xsl:with-param name="liste1" select="substring-after($liste1,',')"/>
<xsl:with-param name="liste2" select="substring-after($liste2,',')"/>
<xsl:with-param name="p" select="$p"/>
</xsl:call-template>
</xsl:when>
<xsl:otherwise>
<xsl:variable name="v1" select="$liste1"/>
<xsl:variable name="v2" select="$liste2"/>
<xsl:value-of select="$v1 + ($v2 - $v1)*$p"/>
</xsl:otherwise>
</xsl:choose>
</xsl:template> 

<xsl:template match="@*[parent::svg:*/child::svg:animate/@attributeName = name()]">
	<xsl:variable name="nodeName" select="name()"/>
	<xsl:variable name="nodeValue" select="."/>
	<xsl:for-each select="parent::svg:*/child::svg:animate[@attributeName= $nodeName ]">
		<xsl:variable name="dur" >
			<xsl:call-template name="simpleTime">
			  <xsl:with-param name="val" select="@dur"/>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="begin0" >
			<xsl:call-template name="simpleTime">
			  <xsl:with-param name="val" select="@begin"/>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="begin" >
		<xsl:choose>
		  <xsl:when test="@repeatCount='indefinite' and $currentTime &gt;= $begin0 ">
			<xsl:value-of select="$begin0 + $dur * floor(($currentTime - $begin0) div $dur)"/>
		  </xsl:when>
		  <xsl:when test="(@repeatCount*$dur  + $begin0)  &gt;=  ($currentTime) and ($currentTime &gt;= $begin0)">
			<xsl:value-of select="$begin0 + $dur * floor(($currentTime - $begin0) div $dur)"/>
		  </xsl:when>
		  <xsl:otherwise>
		  <xsl:value-of select="$begin0"/>
		  </xsl:otherwise>
		</xsl:choose>
		</xsl:variable>
		
		<xsl:variable name="from" select="@from"/><!-- delete use @from everywhere -->
		<xsl:variable name="to" select="@to"/>
		<xsl:choose>
			<xsl:when test="$begin &lt;= $currentTime and ($begin + $dur) &gt; $currentTime">
				<xsl:choose>
				  <xsl:when test="@values">
					<xsl:variable name="x" select="string-length(@values)"/>
					<xsl:variable name="y" select="string-length(translate(@values,';',''))"/>
					<xsl:variable name="z" select="$x - $y"/>
					<xsl:variable name="mult" select="$dur div ($z)"/>
					<xsl:variable name="index" select="floor(($currentTime - $begin) div $mult)"/>
				  
					<xsl:variable name="value1">
						<xsl:call-template name="valueList">
						  <xsl:with-param name="liste" select="@values"/>
						  <xsl:with-param name="i" select="$index"/>
						</xsl:call-template>
					</xsl:variable>
					<xsl:variable name="value2">
						<xsl:call-template name="valueList">
						  <xsl:with-param name="liste" select="@values"/>
						  <xsl:with-param name="i" select="$index + 1"/>
						</xsl:call-template>
					</xsl:variable>
					<xsl:variable name="iv">
					<xsl:call-template name="interpolateList">
					  <xsl:with-param name="liste1" select="$value1"/>
					  <xsl:with-param name="liste2" select="$value2"/>
					  <xsl:with-param name="p" select="($currentTime - ($begin + $index * $mult)) div $mult"/>
					</xsl:call-template>
					</xsl:variable>
					<xsl:attribute name="{$nodeName}">
					  <xsl:value-of select="$iv"/>
					</xsl:attribute>
				  </xsl:when>
				  <xsl:otherwise>
				  <xsl:variable name="iv">
					<xsl:call-template name="interpolateList">
					  <xsl:with-param name="liste1" select="@from"/>
					  <xsl:with-param name="liste2" select="@to"/>
					  <xsl:with-param name="p" select="($currentTime - $begin) div $dur"/>
					</xsl:call-template>
					</xsl:variable>
					<xsl:attribute name="{$nodeName}">
					  <xsl:value-of select="$iv"/>
					</xsl:attribute>
				  </xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="($begin + $dur) &lt; $currentTime and @fill='freeze'">
				<xsl:attribute name="{$nodeName}"><xsl:value-of select="$to"/></xsl:attribute>
			</xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="{$nodeName}"><xsl:value-of select="$nodeValue"/></xsl:attribute>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:for-each>
</xsl:template>

 <xsl:template name="simpleTime">
	<xsl:param name="val"/>
	<xsl:value-of select="substring($val,1,string-length($val)-1)"/>

  </xsl:template>
  
  
 <xsl:template name="valueList">
<xsl:param name="liste"/>
<xsl:param name="i"/>
<xsl:choose>
<xsl:when test="$i=0">


<xsl:choose>
<xsl:when test="contains($liste,';')">
<xsl:value-of select="substring-before($liste,';')"/>
</xsl:when>
<xsl:otherwise>
<xsl:value-of select="$liste"/>
</xsl:otherwise>
</xsl:choose>


</xsl:when>
<xsl:otherwise>
<xsl:call-template name="valueList">
<xsl:with-param name="liste" select="substring-after($liste,';')"/>
<xsl:with-param name="i" select="$i - 1"/>
</xsl:call-template>
</xsl:otherwise>
</xsl:choose>
</xsl:template>       
</xsl:stylesheet>